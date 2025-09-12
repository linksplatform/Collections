#pragma once

#include <cstddef>
#include <memory>
#include <stdexcept>
#include <iterator>
#include <type_traits>
#include <initializer_list>
#include <limits>
#include <cstring>
#include <algorithm>

namespace Platform::Collections
{
    template<typename T, std::size_t StackCapacity = 64>
    class StackVector
    {
    public:
        using value_type = T;
        using size_type = std::size_t;
        using difference_type = std::ptrdiff_t;
        using reference = T&;
        using const_reference = const T&;
        using pointer = T*;
        using const_pointer = const T*;
        using iterator = T*;
        using const_iterator = const T*;
        using reverse_iterator = std::reverse_iterator<iterator>;
        using const_reverse_iterator = std::reverse_iterator<const_iterator>;

    private:
        alignas(T) char stack_buffer_[sizeof(T) * StackCapacity];
        T* heap_buffer_;
        size_type size_;
        size_type capacity_;
        bool using_stack_;

        T* get_buffer() noexcept
        {
            return using_stack_ ? reinterpret_cast<T*>(stack_buffer_) : heap_buffer_;
        }

        const T* get_buffer() const noexcept
        {
            return using_stack_ ? reinterpret_cast<const T*>(stack_buffer_) : heap_buffer_;
        }

        void move_to_heap(size_type new_capacity)
        {
            if (using_stack_)
            {
                heap_buffer_ = static_cast<T*>(std::aligned_alloc(alignof(T), sizeof(T) * new_capacity));
                if (!heap_buffer_)
                {
                    throw std::bad_alloc();
                }

                T* stack_ptr = reinterpret_cast<T*>(stack_buffer_);
                for (size_type i = 0; i < size_; ++i)
                {
                    if constexpr (std::is_move_constructible_v<T>)
                    {
                        new (heap_buffer_ + i) T(std::move(stack_ptr[i]));
                    }
                    else
                    {
                        new (heap_buffer_ + i) T(stack_ptr[i]);
                    }
                    stack_ptr[i].~T();
                }

                using_stack_ = false;
                capacity_ = new_capacity;
            }
        }

        void grow()
        {
            size_type new_capacity = capacity_ == 0 ? StackCapacity : capacity_ * 2;
            
            if (using_stack_ && new_capacity > StackCapacity)
            {
                move_to_heap(new_capacity);
            }
            else if (!using_stack_)
            {
                T* new_buffer = static_cast<T*>(std::aligned_alloc(alignof(T), sizeof(T) * new_capacity));
                if (!new_buffer)
                {
                    throw std::bad_alloc();
                }

                for (size_type i = 0; i < size_; ++i)
                {
                    if constexpr (std::is_move_constructible_v<T>)
                    {
                        new (new_buffer + i) T(std::move(heap_buffer_[i]));
                    }
                    else
                    {
                        new (new_buffer + i) T(heap_buffer_[i]);
                    }
                    heap_buffer_[i].~T();
                }

                std::free(heap_buffer_);
                heap_buffer_ = new_buffer;
                capacity_ = new_capacity;
            }
        }

        void destroy_all()
        {
            T* buffer = get_buffer();
            for (size_type i = 0; i < size_; ++i)
            {
                buffer[i].~T();
            }
        }

    public:
        StackVector() noexcept
            : heap_buffer_(nullptr), size_(0), capacity_(StackCapacity), using_stack_(true)
        {
        }

        explicit StackVector(size_type count)
            : heap_buffer_(nullptr), size_(0), capacity_(StackCapacity), using_stack_(true)
        {
            resize(count);
        }

        StackVector(size_type count, const T& value)
            : heap_buffer_(nullptr), size_(0), capacity_(StackCapacity), using_stack_(true)
        {
            assign(count, value);
        }

        template<typename InputIt>
        StackVector(InputIt first, InputIt last)
            : heap_buffer_(nullptr), size_(0), capacity_(StackCapacity), using_stack_(true)
        {
            assign(first, last);
        }

        StackVector(std::initializer_list<T> init)
            : heap_buffer_(nullptr), size_(0), capacity_(StackCapacity), using_stack_(true)
        {
            assign(init);
        }

        StackVector(const StackVector& other)
            : heap_buffer_(nullptr), size_(0), capacity_(StackCapacity), using_stack_(true)
        {
            assign(other.begin(), other.end());
        }

        StackVector(StackVector&& other) noexcept
            : heap_buffer_(other.heap_buffer_), size_(other.size_), capacity_(other.capacity_), using_stack_(other.using_stack_)
        {
            if (other.using_stack_)
            {
                T* other_buffer = reinterpret_cast<T*>(other.stack_buffer_);
                T* this_buffer = reinterpret_cast<T*>(stack_buffer_);
                
                for (size_type i = 0; i < size_; ++i)
                {
                    new (this_buffer + i) T(std::move(other_buffer[i]));
                    other_buffer[i].~T();
                }
            }
            
            other.heap_buffer_ = nullptr;
            other.size_ = 0;
            other.capacity_ = StackCapacity;
            other.using_stack_ = true;
        }

        ~StackVector()
        {
            destroy_all();
            if (!using_stack_ && heap_buffer_)
            {
                std::free(heap_buffer_);
            }
        }

        StackVector& operator=(const StackVector& other)
        {
            if (this != &other)
            {
                clear();
                assign(other.begin(), other.end());
            }
            return *this;
        }

        StackVector& operator=(StackVector&& other) noexcept
        {
            if (this != &other)
            {
                destroy_all();
                if (!using_stack_ && heap_buffer_)
                {
                    std::free(heap_buffer_);
                }

                heap_buffer_ = other.heap_buffer_;
                size_ = other.size_;
                capacity_ = other.capacity_;
                using_stack_ = other.using_stack_;

                if (other.using_stack_)
                {
                    T* other_buffer = reinterpret_cast<T*>(other.stack_buffer_);
                    T* this_buffer = reinterpret_cast<T*>(stack_buffer_);
                    
                    for (size_type i = 0; i < size_; ++i)
                    {
                        new (this_buffer + i) T(std::move(other_buffer[i]));
                        other_buffer[i].~T();
                    }
                }

                other.heap_buffer_ = nullptr;
                other.size_ = 0;
                other.capacity_ = StackCapacity;
                other.using_stack_ = true;
            }
            return *this;
        }

        StackVector& operator=(std::initializer_list<T> ilist)
        {
            assign(ilist);
            return *this;
        }

        void assign(size_type count, const T& value)
        {
            clear();
            reserve(count);
            for (size_type i = 0; i < count; ++i)
            {
                push_back(value);
            }
        }

        template<typename InputIt>
        void assign(InputIt first, InputIt last)
        {
            clear();
            for (auto it = first; it != last; ++it)
            {
                push_back(*it);
            }
        }

        void assign(std::initializer_list<T> ilist)
        {
            assign(ilist.begin(), ilist.end());
        }

        reference at(size_type pos)
        {
            if (pos >= size_)
            {
                throw std::out_of_range("StackVector::at: index out of range");
            }
            return get_buffer()[pos];
        }

        const_reference at(size_type pos) const
        {
            if (pos >= size_)
            {
                throw std::out_of_range("StackVector::at: index out of range");
            }
            return get_buffer()[pos];
        }

        reference operator[](size_type pos) noexcept
        {
            return get_buffer()[pos];
        }

        const_reference operator[](size_type pos) const noexcept
        {
            return get_buffer()[pos];
        }

        reference front() noexcept
        {
            return get_buffer()[0];
        }

        const_reference front() const noexcept
        {
            return get_buffer()[0];
        }

        reference back() noexcept
        {
            return get_buffer()[size_ - 1];
        }

        const_reference back() const noexcept
        {
            return get_buffer()[size_ - 1];
        }

        T* data() noexcept
        {
            return get_buffer();
        }

        const T* data() const noexcept
        {
            return get_buffer();
        }

        iterator begin() noexcept
        {
            return get_buffer();
        }

        const_iterator begin() const noexcept
        {
            return get_buffer();
        }

        const_iterator cbegin() const noexcept
        {
            return get_buffer();
        }

        iterator end() noexcept
        {
            return get_buffer() + size_;
        }

        const_iterator end() const noexcept
        {
            return get_buffer() + size_;
        }

        const_iterator cend() const noexcept
        {
            return get_buffer() + size_;
        }

        reverse_iterator rbegin() noexcept
        {
            return reverse_iterator(end());
        }

        const_reverse_iterator rbegin() const noexcept
        {
            return const_reverse_iterator(end());
        }

        const_reverse_iterator crbegin() const noexcept
        {
            return const_reverse_iterator(end());
        }

        reverse_iterator rend() noexcept
        {
            return reverse_iterator(begin());
        }

        const_reverse_iterator rend() const noexcept
        {
            return const_reverse_iterator(begin());
        }

        const_reverse_iterator crend() const noexcept
        {
            return const_reverse_iterator(begin());
        }

        bool empty() const noexcept
        {
            return size_ == 0;
        }

        size_type size() const noexcept
        {
            return size_;
        }

        size_type max_size() const noexcept
        {
            return std::numeric_limits<size_type>::max() / sizeof(T);
        }

        void reserve(size_type new_cap)
        {
            if (new_cap > capacity_)
            {
                if (using_stack_ && new_cap > StackCapacity)
                {
                    move_to_heap(new_cap);
                }
                else if (!using_stack_)
                {
                    T* new_buffer = static_cast<T*>(std::aligned_alloc(alignof(T), sizeof(T) * new_cap));
                    if (!new_buffer)
                    {
                        throw std::bad_alloc();
                    }

                    for (size_type i = 0; i < size_; ++i)
                    {
                        if constexpr (std::is_move_constructible_v<T>)
                        {
                            new (new_buffer + i) T(std::move(heap_buffer_[i]));
                        }
                        else
                        {
                            new (new_buffer + i) T(heap_buffer_[i]);
                        }
                        heap_buffer_[i].~T();
                    }

                    std::free(heap_buffer_);
                    heap_buffer_ = new_buffer;
                    capacity_ = new_cap;
                }
            }
        }

        size_type capacity() const noexcept
        {
            return capacity_;
        }

        void shrink_to_fit()
        {
            if (!using_stack_ && size_ <= StackCapacity)
            {
                T* buffer = get_buffer();
                T* stack_ptr = reinterpret_cast<T*>(stack_buffer_);
                
                for (size_type i = 0; i < size_; ++i)
                {
                    if constexpr (std::is_move_constructible_v<T>)
                    {
                        new (stack_ptr + i) T(std::move(buffer[i]));
                    }
                    else
                    {
                        new (stack_ptr + i) T(buffer[i]);
                    }
                    buffer[i].~T();
                }

                std::free(heap_buffer_);
                heap_buffer_ = nullptr;
                using_stack_ = true;
                capacity_ = StackCapacity;
            }
        }

        void clear() noexcept
        {
            destroy_all();
            size_ = 0;
        }

        iterator insert(const_iterator pos, const T& value)
        {
            return insert(pos, 1, value);
        }

        iterator insert(const_iterator pos, T&& value)
        {
            size_type index = pos - begin();
            if (size_ == capacity_)
            {
                grow();
            }

            T* buffer = get_buffer();
            for (size_type i = size_; i > index; --i)
            {
                if constexpr (std::is_move_constructible_v<T>)
                {
                    new (buffer + i) T(std::move(buffer[i - 1]));
                }
                else
                {
                    new (buffer + i) T(buffer[i - 1]);
                }
                buffer[i - 1].~T();
            }

            new (buffer + index) T(std::move(value));
            ++size_;
            return buffer + index;
        }

        iterator insert(const_iterator pos, size_type count, const T& value)
        {
            size_type index = pos - begin();
            if (size_ + count > capacity_)
            {
                reserve(size_ + count);
            }

            T* buffer = get_buffer();
            for (size_type i = size_ + count - 1; i >= index + count; --i)
            {
                if constexpr (std::is_move_constructible_v<T>)
                {
                    new (buffer + i) T(std::move(buffer[i - count]));
                }
                else
                {
                    new (buffer + i) T(buffer[i - count]);
                }
                buffer[i - count].~T();
            }

            for (size_type i = 0; i < count; ++i)
            {
                new (buffer + index + i) T(value);
            }

            size_ += count;
            return buffer + index;
        }

        template<typename InputIt>
        iterator insert(const_iterator pos, InputIt first, InputIt last)
        {
            size_type index = pos - begin();
            size_type count = std::distance(first, last);
            
            if (size_ + count > capacity_)
            {
                reserve(size_ + count);
            }

            T* buffer = get_buffer();
            for (size_type i = size_ + count - 1; i >= index + count; --i)
            {
                if constexpr (std::is_move_constructible_v<T>)
                {
                    new (buffer + i) T(std::move(buffer[i - count]));
                }
                else
                {
                    new (buffer + i) T(buffer[i - count]);
                }
                buffer[i - count].~T();
            }

            size_type i = index;
            for (auto it = first; it != last; ++it, ++i)
            {
                new (buffer + i) T(*it);
            }

            size_ += count;
            return buffer + index;
        }

        iterator insert(const_iterator pos, std::initializer_list<T> ilist)
        {
            return insert(pos, ilist.begin(), ilist.end());
        }

        template<typename... Args>
        iterator emplace(const_iterator pos, Args&&... args)
        {
            size_type index = pos - begin();
            if (size_ == capacity_)
            {
                grow();
            }

            T* buffer = get_buffer();
            for (size_type i = size_; i > index; --i)
            {
                if constexpr (std::is_move_constructible_v<T>)
                {
                    new (buffer + i) T(std::move(buffer[i - 1]));
                }
                else
                {
                    new (buffer + i) T(buffer[i - 1]);
                }
                buffer[i - 1].~T();
            }

            new (buffer + index) T(std::forward<Args>(args)...);
            ++size_;
            return buffer + index;
        }

        iterator erase(const_iterator pos)
        {
            size_type index = pos - begin();
            T* buffer = get_buffer();
            
            buffer[index].~T();
            for (size_type i = index; i < size_ - 1; ++i)
            {
                if constexpr (std::is_move_constructible_v<T>)
                {
                    new (buffer + i) T(std::move(buffer[i + 1]));
                }
                else
                {
                    new (buffer + i) T(buffer[i + 1]);
                }
                buffer[i + 1].~T();
            }

            --size_;
            return buffer + index;
        }

        iterator erase(const_iterator first, const_iterator last)
        {
            size_type first_index = first - begin();
            size_type last_index = last - begin();
            size_type count = last_index - first_index;

            T* buffer = get_buffer();
            for (size_type i = first_index; i < last_index; ++i)
            {
                buffer[i].~T();
            }

            for (size_type i = last_index; i < size_; ++i)
            {
                if constexpr (std::is_move_constructible_v<T>)
                {
                    new (buffer + i - count) T(std::move(buffer[i]));
                }
                else
                {
                    new (buffer + i - count) T(buffer[i]);
                }
                buffer[i].~T();
            }

            size_ -= count;
            return buffer + first_index;
        }

        void push_back(const T& value)
        {
            if (size_ == capacity_)
            {
                grow();
            }
            new (get_buffer() + size_) T(value);
            ++size_;
        }

        void push_back(T&& value)
        {
            if (size_ == capacity_)
            {
                grow();
            }
            new (get_buffer() + size_) T(std::move(value));
            ++size_;
        }

        template<typename... Args>
        reference emplace_back(Args&&... args)
        {
            if (size_ == capacity_)
            {
                grow();
            }
            T* ptr = get_buffer() + size_;
            new (ptr) T(std::forward<Args>(args)...);
            ++size_;
            return *ptr;
        }

        void pop_back()
        {
            if (size_ > 0)
            {
                --size_;
                get_buffer()[size_].~T();
            }
        }

        void resize(size_type count)
        {
            if (count > size_)
            {
                reserve(count);
                T* buffer = get_buffer();
                for (size_type i = size_; i < count; ++i)
                {
                    new (buffer + i) T();
                }
            }
            else if (count < size_)
            {
                T* buffer = get_buffer();
                for (size_type i = count; i < size_; ++i)
                {
                    buffer[i].~T();
                }
            }
            size_ = count;
        }

        void resize(size_type count, const T& value)
        {
            if (count > size_)
            {
                reserve(count);
                T* buffer = get_buffer();
                for (size_type i = size_; i < count; ++i)
                {
                    new (buffer + i) T(value);
                }
            }
            else if (count < size_)
            {
                T* buffer = get_buffer();
                for (size_type i = count; i < size_; ++i)
                {
                    buffer[i].~T();
                }
            }
            size_ = count;
        }

        void swap(StackVector& other) noexcept
        {
            if (using_stack_ && other.using_stack_)
            {
                char temp_buffer[sizeof(T) * StackCapacity];
                memcpy(temp_buffer, stack_buffer_, sizeof(T) * size_);
                memcpy(stack_buffer_, other.stack_buffer_, sizeof(T) * other.size_);
                memcpy(other.stack_buffer_, temp_buffer, sizeof(T) * size_);
            }
            else
            {
                std::swap(heap_buffer_, other.heap_buffer_);
                if (using_stack_ != other.using_stack_)
                {
                    if (using_stack_)
                    {
                        memcpy(other.stack_buffer_, stack_buffer_, sizeof(T) * size_);
                    }
                    else
                    {
                        memcpy(stack_buffer_, other.stack_buffer_, sizeof(T) * other.size_);
                    }
                }
            }

            std::swap(size_, other.size_);
            std::swap(capacity_, other.capacity_);
            std::swap(using_stack_, other.using_stack_);
        }

        bool is_using_stack() const noexcept
        {
            return using_stack_;
        }

        static constexpr size_type stack_capacity() noexcept
        {
            return StackCapacity;
        }
    };

    template<typename T, std::size_t N>
    bool operator==(const StackVector<T, N>& lhs, const StackVector<T, N>& rhs)
    {
        if (lhs.size() != rhs.size())
            return false;
        
        return std::equal(lhs.begin(), lhs.end(), rhs.begin());
    }

    template<typename T, std::size_t N>
    bool operator!=(const StackVector<T, N>& lhs, const StackVector<T, N>& rhs)
    {
        return !(lhs == rhs);
    }

    template<typename T, std::size_t N>
    bool operator<(const StackVector<T, N>& lhs, const StackVector<T, N>& rhs)
    {
        return std::lexicographical_compare(lhs.begin(), lhs.end(), rhs.begin(), rhs.end());
    }

    template<typename T, std::size_t N>
    bool operator<=(const StackVector<T, N>& lhs, const StackVector<T, N>& rhs)
    {
        return !(rhs < lhs);
    }

    template<typename T, std::size_t N>
    bool operator>(const StackVector<T, N>& lhs, const StackVector<T, N>& rhs)
    {
        return rhs < lhs;
    }

    template<typename T, std::size_t N>
    bool operator>=(const StackVector<T, N>& lhs, const StackVector<T, N>& rhs)
    {
        return !(lhs < rhs);
    }

    template<typename T, std::size_t N>
    void swap(StackVector<T, N>& lhs, StackVector<T, N>& rhs) noexcept
    {
        lhs.swap(rhs);
    }
}
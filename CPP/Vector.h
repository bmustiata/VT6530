#ifndef _vector_h
#define _vector_h


template <class T>
class Vector
{
	T **array;
	int used;
	int len;

public:

	Vector()
	{
		array = new T *[16];
		used = 0;
		len = 16;
	}

	Vector(int size) 
	{
		array = new T *[size];
		used = 0;
		len = size;
	}

	virtual ~Vector()
	{
		_ASSERT(_CrtIsMemoryBlock(array, len * sizeof(T *), 0, 0, 0));
		delete[] array;
	}

	T *pop()
	{
		_ASSERT(_CrtIsMemoryBlock(array, len * sizeof(T *), 0, 0, 0));
		if (used > 0) 
		{
		    return array[--used];
		}
		else
		{
		    return NULL;
		}		
	}
	
	int capacity()
	{
		_ASSERT(_CrtIsMemoryBlock(array, len * sizeof(T *), 0, 0, 0));
		return len;
	}
	
	void addElement(T *o) 
	{
		_ASSERT(_CrtIsMemoryBlock(array, len * sizeof(T *), 0, 0, 0));
		extend();
		array[used++] = o;
	}
	
	void extend() 
	{
		if (used >= len) 
		{
			int space = len;
			
			while (used >= space) 
			{
				space <<= 1;
			}
			T **array2 = new T *[space];
			int i;
			for (i = 0; i < used; i++) 
			{
				array2[i] = array[i];
			}
			for (i = used; (i < space); i++) 
			{
				array2[i] = NULL;
			}
			len = space;
			delete array;
			array = array2;
		}
	}
	
	int size() 
	{
		return used;
	}
		
	void clear()
	{
		_ASSERT(_CrtIsMemoryBlock(array, len * sizeof(T *), 0, 0, 0));
		used = 0;
	}
	
	T *elementAt(int at) 
	{
		_ASSERT(_CrtIsMemoryBlock(array, len * sizeof(T *), 0, 0, 0));
		_ASSERT(_CrtIsValidHeapPointer(array[at]));
		return array[at];
	}

	void setElementAt(int at, T *o) 
	{
		_ASSERT(_CrtIsMemoryBlock(array, len * sizeof(T *), 0, 0, 0));
		_ASSERT(_CrtIsValidHeapPointer(array[at]));
		array[at] = o;
	}

	T *firstElement() 
	{
		_ASSERT(_CrtIsMemoryBlock(array, len * sizeof(T *), 0, 0, 0));
		_ASSERT(_CrtIsValidHeapPointer(array[0]));
		if (used > 0) 
		{
			return array[0];
		} 
		else 
		{
			return NULL;
		}
	}
	
	T *lastElement()
	{
		_ASSERT(_CrtIsMemoryBlock(array, len * sizeof(T *), 0, 0, 0));
		_ASSERT(_CrtIsValidHeapPointer(array[used-1]));
		if (used > 0) 
		{
			return array[used-1];
		}
		else
		{
			return NULL;
		}
	}
	
	void removeElement() 
	{
		removeElementAt(0);
	}
	
	void removeElement(T *o) 
	{
		int i;
		for (i = 0; i < used; i++) 
		{
			if (array[i] == o) 
			{
				removeElementAt(i);
				return;
			}
		}
	}
	
	int indexOf(T *o) 
	{
		int i;
		for (i = 0; (i < used); i++) 
		{
			if (array[i] == o) 
			{
				return i;
			}
		}
		return -1;
	}

	void removeElementAt(int at) 
	{
		_ASSERT(_CrtIsMemoryBlock(array, len * sizeof(T *), 0, 0, 0));

		int i;
		for (i = at; (i < (used - 1)); i++) 
		{
			_ASSERT(_CrtIsValidHeapPointer(array[i]));
			array[i] = array[i + 1];
		}
		used--;
	}
	
	void insertElementAt(T *o, int at) 
	{
		_ASSERT(_CrtIsMemoryBlock(array, len * sizeof(T *), 0, 0, 0));
		_ASSERT(_CrtIsValidHeapPointer(array[at]));

		int i;
		
		extend();
		for (i = used; (i > at); i--) 
		{
			array[i] = array[i - 1];
		}
		array[at] = o;
		used++;
	}
};

#endif
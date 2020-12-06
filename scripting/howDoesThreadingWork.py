import queue
from threading import Thread

def foo(que_ret,bar):
    print('hello ' + str(bar))
    que_ret.put('foo')
    #return('foo')

#t = Thread(target=lambda q, arg1: q.put(foo(arg1)), args=(que, 'world!'))
que_ret = queue.Queue()
t=Thread(target=foo, args=([que_ret, 42]), daemon=True)
t.start()
#t.join()
result = que_ret.get()
print(result)
print(que_ret.qsize())

a=[1, 2, 3]

def bar(a):
    a.pop()

bar(a)
print(a)
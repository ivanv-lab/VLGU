def f(a):
    return '1' if ((not a[0]) or (not a[1])) else '0'
	
def truthTable(i=-1, value=None, a=None):
    if value==None:
        a = [0]*4
        truthTable(0, 0, a)
        truthTable(0, 1, a)
    else:
        a[i] = value
        if i==3:
            print(' | ', a[0], ' | ', a[1], ' | ', a[2], ' | ', a[3], ' | ',  f(a))
        else:
            truthTable(i+1, 0, a)
            truthTable(i+1, 1, a)

print(' |  A  |  B  |  C  |  D  |  F  ') 
truthTable()
input('press any key')

import base64
from Crypto.Util.number import *
import random
import  socket, json, time

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.bind(("127.0.0.1", 1116))
server.listen()
client, add_client = server.accept()

#recieving 'n, V' from Trent
mess_from_Trent = client.recv(1024)
data = json.loads(mess_from_Trent.decode("utf-8"))
n = data["n_modulus"]
v = data["V_key"]
print (f"n = {n}")
print (f"V = {v}")

count = 0
iterations = 3
for i in range (iterations):
    time.sleep(2)
    server2 = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server2.bind(("127.0.0.1", 1113))
    server2.listen()
    client2, add_client2 = server2.accept()

    print("------------------")
    print(f"Iteration number: {i+1}")
    #recieving 'x' from Alice
    mess_from_Alice = client2.recv(1024)
    data = json.loads(mess_from_Alice.decode("utf-8"))
    x = data["x_secret"]
    print (f"x = {x}")

    #gen 'e'
    e = random.randint(0, 1)
    print (f"e = {e}")

    time.sleep(4)
    client3 = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    client3.connect(("127.0.0.1", 1114))

    data = {
        "bit_e" : e
    }
    mess_to_Alice = json.dumps(data)

    #sendind 'e' to Alice
    client3.send(mess_to_Alice.encode("utf-8"))

    time.sleep(2)
    server3 = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server3.bind(("127.0.0.1", 1115))
    server3.listen()
    client4, add_client3 = server3.accept()

    #recieving 'y' from Alice
    mess_from_Alice2 = client4.recv(1024)
    data = json.loads(mess_from_Alice2.decode("utf-8"))
    y = data["y_number"]
    print (f"x = {x}")

    if (y == 0):
        print("Alice has no secret!")
        raise SystemExit

    #checking knowledge
    if (pow(y,2,n) == (x*(v**e))%n):
        print ("True")
        count +=1
    else:
        print ("False")


if (count == iterations):
    print ("Alice has secret")
else:
    print("Alice has no secret")              
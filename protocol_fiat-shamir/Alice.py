import base64
from Crypto.Util.number import *
import random
import  socket, json, time

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.bind(("127.0.0.1", 1111))
server.listen()
client, add_client = server.accept()

#recieving 'n' from Trent
mess_from_Trent = client.recv(1024)
data = json.loads(mess_from_Trent.decode("utf-8"))
n = data["n_modulus"]
print (n)

#choose 's' in interval
s = getRandomRange(1, n-1)

#check is 's' prime number
if (GCD(s, n) != 1):
    while (GCD(s,n) != 1):
        s = getRandomRange(1, n-1)

print(f"s = {s}")

#calculate 'V'
v = pow(s, 2, n)
print(f"V = {v}")

time.sleep(2)
client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
client.connect(("127.0.0.1", 1112))

data = {
    "V_key" : v
}
mess_to_Trent = json.dumps(data)

#sendind 'V' to Trent
client.send(mess_to_Trent.encode("utf-8"))
client.close()

iterations = 3
for i in range(iterations):
    print("------------------")
    print(f"Iteration number: {i+1}")
    #generate 'r'
    r = getRandomRange(1, n-1)
    print(f"r = {r}")

    #calc 'x'
    x = pow(r, 2, n)
    print (f"x = {x}")

    time.sleep(4)
    client2 = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    client2.connect(("127.0.0.1", 1113))

    data = {
        "x_secret" : x
    }
    mess_to_Bob = json.dumps(data)

    #sendind 'x' to Bob
    client2.send(mess_to_Bob.encode("utf-8"))

    time.sleep(2)
    server2 = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server2.bind(("127.0.0.1", 1114))
    server2.listen()
    client3, add_client2 = server2.accept()

    #recieving 'e' from Bob
    mess_from_Bob = client3.recv(1024)
    data = json.loads(mess_from_Bob.decode("utf-8"))
    e = data["bit_e"]
    print (f"e = {e}")

    #calc 'y'
    y = (r*(s**e))%n 
    print(f"y = {y}")

    time.sleep(2)
    client3 = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    client3.connect(("127.0.0.1", 1115))

    data = {
        "y_number" : y
    }
    mess_to_Bob2 = json.dumps(data)

    #sendind 'y' to Bob
    client3.send(mess_to_Bob2.encode("utf-8"))
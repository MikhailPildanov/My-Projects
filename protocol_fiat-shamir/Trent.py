import base64
from msilib.schema import Environment
from Crypto.Util.number import *
import random
import  socket
import json,time

#generate prime numbers: p, q
p = getPrime(10)
q = getPrime(10)
n = p*q
print(f"p = {p}, q = {q}, n = {n}")

client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
client.connect(("127.0.0.1", 1111))

data = {
    "n_modulus" : n
}
mess_to_Alice = json.dumps(data)

#sendind 'n' to Alice
client.send(mess_to_Alice.encode("utf-8"))

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.bind(("127.0.0.1", 1112))
server.listen()
client, add_client = server.accept()

#recieving 'V' from Alice
mess_from_Trent = client.recv(1024)
data = json.loads(mess_from_Trent.decode("utf-8"))
v = data["V_key"]
print (f"V = {v}")  

client2 = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
client2.connect(("127.0.0.1", 1116))

data = {
    "n_modulus" : n,
    "V_key" : v
}
mess_to_Bob = json.dumps(data)

#sendind 'n, v' to Bob
client2.send(mess_to_Bob.encode("utf-8"))

time.sleep(10)

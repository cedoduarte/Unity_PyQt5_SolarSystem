# This Python file uses the following encoding: utf-8
from PyQt5.QtNetwork import *
from PyQt5.QtCore import *

class Server(QTcpServer):
    socket = None
    messageSent = pyqtSignal(str)
    dataReceived = pyqtSignal(str)
    newClientConnected = pyqtSignal()
    clientDisconnected = pyqtSignal()

    def __init__(self, parent=None):
        QTcpServer.__init__(self, parent)

    def incomingConnection(self, handle):
        if self.socket == None:
            self.socket = self.makeSocket(handle)
            self.newClientConnected.emit()

    def makeSocket(self, handle):
        sock = QTcpSocket(self)
        sock.setSocketDescriptor(handle)
        sock.readyRead.connect(self.onClientReadyRead)
        sock.disconnected.connect(self.onClientDisconnected)
        return sock

    def onClientReadyRead(self):
        data = self.socket.readAll()
        data = str(data, "utf-8")
        self.dataReceived.emit(data)

    def onClientDisconnected(self):
        self.socket = None
        self.clientDisconnected.emit()

    def sendMessage(self, message):
        if self.socket == None:
            return
        self.socket.write(message.encode("utf-8"))
        self.messageSent.emit(message)

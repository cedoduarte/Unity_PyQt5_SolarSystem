# This Python file uses the following encoding: utf-8
from PyQt5.QtNetwork import *
from PyQt5.QtWidgets import *
from PyQt5.QtCore import *
from PyQt5.QtGui import *
from PyQt5 import uic
from server import Server

class MainWindow(QMainWindow):
    server = None
    validator = None
    serverRunning = False

    def __init__(self, parent=None):
        QMainWindow.__init__(self, parent)
        uic.loadUi("mainwindow.ui", self)
        self.server = self.makeServer()
        self.validator = self.makeValidator()
        self.send_pushButton.setDisabled(True)
        self.start_pushButton.clicked.connect(self.onStartClicked)
        self.send_pushButton.clicked.connect(self.onSendClicked)

    def closeEvent(self, e):
        self.sendMessage("_to_unity_abort_")

    def makeValidator(self):
        validator = QDoubleValidator(self)
        validator.setLocale(QLocale(QLocale.English, QLocale.UnitedStates))

        self.mercury_rot_vel_lineEdit.setValidator(validator)
        self.mercury_trans_vel_lineEdit.setValidator(validator)

        self.venus_rot_vel_lineEdit.setValidator(validator)
        self.venus_trans_vel_lineEdit.setValidator(validator)

        self.earth_rot_vel_lineEdit.setValidator(validator)
        self.earth_trans_vel_lineEdit.setValidator(validator)

        self.mars_rot_vel_lineEdit.setValidator(validator)
        self.mars_trans_vel_lineEdit.setValidator(validator)

        self.jupiter_rot_vel_lineEdit.setValidator(validator)
        self.jupiter_trans_vel_lineEdit.setValidator(validator)

        self.saturn_rot_vel_lineEdit.setValidator(validator)
        self.saturn_trans_vel_lineEdit.setValidator(validator)

        self.uranus_rot_vel_lineEdit.setValidator(validator)
        self.uranus_trans_vel_lineEdit.setValidator(validator)

        self.neptune_rot_vel_lineEdit.setValidator(validator)
        self.neptune_trans_vel_lineEdit.setValidator(validator)

        return validator

    def onSendClicked(self):
        mercury_rot_vel = self.mercury_rot_vel_lineEdit.text()
        mercury_trans_vel = self.mercury_trans_vel_lineEdit.text()

        venus_rot_vel = self.venus_rot_vel_lineEdit.text()
        venus_trans_vel = self.venus_trans_vel_lineEdit.text()

        earth_rot_vel = self.earth_rot_vel_lineEdit.text()
        earth_trans_vel = self.earth_trans_vel_lineEdit.text()

        mars_rot_vel = self.mars_rot_vel_lineEdit.text()
        mars_trans_vel = self.mars_trans_vel_lineEdit.text()

        jupiter_rot_vel = self.jupiter_rot_vel_lineEdit.text()
        jupiter_trans_vel = self.jupiter_trans_vel_lineEdit.text()

        saturn_rot_vel = self.saturn_rot_vel_lineEdit.text()
        saturn_trans_vel = self.saturn_trans_vel_lineEdit.text()

        uranus_rot_vel = self.uranus_rot_vel_lineEdit.text()
        uranus_trans_vel = self.uranus_trans_vel_lineEdit.text()

        neptune_rot_vel = self.neptune_rot_vel_lineEdit.text()
        neptune_trans_vel = self.neptune_trans_vel_lineEdit.text()

        data = "_to_unity_parameters_(mercury_rot_vel={0};mercury_trans_vel={1};"
        data += "venus_rot_vel={2};venus_trans_vel={3};"
        data += "earth_rot_vel={4};earth_trans_vel={5};"
        data += "mars_rot_vel={6};mars_trans_vel={7};"
        data += "jupiter_rot_vel={8};jupiter_trans_vel={9};"
        data += "saturn_rot_vel={10};saturn_trans_vel={11};"
        data += "uranus_rot_vel={12};uranus_trans_vel={13};"
        data += "neptune_rot_vel={14};neptune_trans_vel={15}"
        data = data.format(mercury_rot_vel, mercury_trans_vel,
                           venus_rot_vel, venus_trans_vel,
                           earth_rot_vel, earth_trans_vel,
                           mars_rot_vel, mars_trans_vel,
                           jupiter_rot_vel, jupiter_trans_vel,
                           saturn_rot_vel, saturn_trans_vel,
                           uranus_rot_vel, uranus_trans_vel,
                           neptune_rot_vel, neptune_trans_vel)
        self.server.sendMessage(data)

    def onStartClicked(self):
        if self.serverRunning:
            return
        port = self.port_lineEdit.text()
        port = int(port)
        if self.server.listen(QHostAddress.Any, port):
            self.serverRunning = True
            self.plainTextEdit.appendPlainText("Listening...")
        else:
            self.plainTextEdit.appendPlainText("Not listening")

    def onNewClientConnected(self):
        self.plainTextEdit.appendPlainText("New client connected...")

    def onClientDisconnected(self):
        self.plainTextEdit.appendPlainText("Client disconnected...")

    def onDataReceived(self, data):
        self.plainTextEdit.appendPlainText("From client: " + data)

        if "_to_pyqt_parameters_(" in data:
            txt = data.replace("_to_pyqt_parameters_(", "")
            txt = txt.replace(")", "")
            args = txt.split(";")

            mercury_rot_vel = args[0]
            mercury_trans_vel = args[1]

            venus_rot_vel = args[2]
            venus_trans_vel = args[3]

            earth_rot_vel = args[4]
            earth_trans_vel = args[5]

            mars_rot_vel = args[6]
            mars_trans_vel = args[7]

            jupiter_rot_vel = args[8]
            jupiter_trans_vel = args[9]

            saturn_rot_vel = args[10]
            saturn_trans_vel = args[11]

            uranus_rot_vel = args[12]
            uranus_trans_vel = args[13]

            neptune_rot_vel = args[14]
            neptune_trans_vel = args[15]

            mercury_rot_vel = mercury_rot_vel[mercury_rot_vel.find("=")+1:]
            mercury_trans_vel = mercury_trans_vel[mercury_trans_vel.find("=")+1:]

            venus_rot_vel = venus_rot_vel[venus_rot_vel.find("=")+1:]
            venus_trans_vel = venus_trans_vel[venus_trans_vel.find("=")+1:]

            earth_rot_vel = earth_rot_vel[earth_rot_vel.find("=")+1:]
            earth_trans_vel = earth_trans_vel[earth_trans_vel.find("=")+1:]

            mars_rot_vel = mars_rot_vel[mars_rot_vel.find("=")+1:]
            mars_trans_vel = mars_trans_vel[mars_trans_vel.find("=")+1:]

            jupiter_rot_vel = jupiter_rot_vel[jupiter_rot_vel.find("=")+1:]
            jupiter_trans_vel = jupiter_trans_vel[jupiter_trans_vel.find("=")+1:]

            saturn_rot_vel = saturn_rot_vel[saturn_rot_vel.find("=")+1:]
            saturn_trans_vel = saturn_trans_vel[saturn_trans_vel.find("=")+1:]

            uranus_rot_vel = uranus_rot_vel[uranus_rot_vel.find("=")+1:]
            uranus_trans_vel = uranus_trans_vel[uranus_trans_vel.find("=")+1:]

            neptune_rot_vel = neptune_rot_vel[neptune_rot_vel.find("=")+1:]
            neptune_trans_vel = neptune_trans_vel[neptune_trans_vel.find("=")+1:]

            mercury_rot_vel = mercury_rot_vel.replace(",", ".")
            mercury_trans_vel = mercury_trans_vel.replace(",", ".")

            venus_rot_vel = venus_rot_vel.replace(",", ".")
            venus_trans_vel = venus_trans_vel.replace(",", ".")

            earth_rot_vel = earth_rot_vel.replace(",", ".")
            earth_trans_vel = earth_trans_vel.replace(",", ".")

            mars_rot_vel = mars_rot_vel.replace(",", ".")
            mars_trans_vel = mars_trans_vel.replace(",", ".")

            jupiter_rot_vel = jupiter_rot_vel.replace(",", ".")
            jupiter_trans_vel = jupiter_trans_vel.replace(",", ".")

            saturn_rot_vel = saturn_rot_vel.replace(",", ".")
            saturn_trans_vel = saturn_trans_vel.replace(",", ".")

            uranus_rot_vel = uranus_rot_vel.replace(",", ".")
            uranus_trans_vel = uranus_trans_vel.replace(",", ".")

            neptune_rot_vel = neptune_rot_vel.replace(",", ".")
            neptune_trans_vel = neptune_trans_vel.replace(",", ".")

            self.mercury_rot_vel_lineEdit.setText(mercury_rot_vel)
            self.mercury_trans_vel_lineEdit.setText(mercury_trans_vel)

            self.venus_rot_vel_lineEdit.setText(venus_rot_vel)
            self.venus_trans_vel_lineEdit.setText(venus_trans_vel)

            self.earth_rot_vel_lineEdit.setText(earth_rot_vel)
            self.earth_trans_vel_lineEdit.setText(earth_trans_vel)

            self.mars_rot_vel_lineEdit.setText(mars_rot_vel)
            self.mars_trans_vel_lineEdit.setText(mars_trans_vel)

            self.jupiter_rot_vel_lineEdit.setText(jupiter_rot_vel)
            self.jupiter_trans_vel_lineEdit.setText(jupiter_trans_vel)

            self.saturn_rot_vel_lineEdit.setText(saturn_rot_vel)
            self.saturn_trans_vel_lineEdit.setText(saturn_trans_vel)

            self.uranus_rot_vel_lineEdit.setText(uranus_rot_vel)
            self.uranus_trans_vel_lineEdit.setText(uranus_trans_vel)

            self.neptune_rot_vel_lineEdit.setText(neptune_rot_vel)
            self.neptune_trans_vel_lineEdit.setText(neptune_trans_vel)

            self.send_pushButton.setEnabled(True)

    def onMessageSent(self, data):
        self.plainTextEdit.appendPlainText("Message sent: " + data)

    def makeServer(self):
        server = Server(self)
        server.newClientConnected.connect(self.onNewClientConnected)
        server.clientDisconnected.connect(self.onClientDisconnected)
        server.dataReceived.connect(self.onDataReceived)
        server.messageSent.connect(self.onMessageSent)
        return server

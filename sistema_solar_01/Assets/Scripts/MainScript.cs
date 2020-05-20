using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Threading;

public class MainScript : MonoBehaviour
{
    public TcpClient socket;
    public Thread thread;

    // Mercury
    public GameObject Mercury;
    public GameObject MercuryTurningPoint;
    public float MercuryRotationVelocity;
    public float MercuryTranslationVelocity;

    // Venus
    public GameObject Venus;
    public GameObject VenusTurningPoint;
    public float VenusRotationVelocity;
    public float VenusTranslationVelocity;

    // Earth
    public GameObject Earth;
    public GameObject EarthTurningPoint;
    public float EarthRotationVelocity;
    public float EarthTranslationVelocity;

    // Mars
    public GameObject Mars;
    public GameObject MarsTurningPoint;
    public float MarsRotationVelocity;
    public float MarsTranslationVelocity;

    // Jupiter
    public GameObject Jupiter;
    public GameObject JupiterTurningPoint;
    public float JupiterRotationVelocity;
    public float JupiterTranslationVelocity;

    // Saturn
    public GameObject Saturn;
    public GameObject SaturnTurningPoint;
    public float SaturnRotationVelocity;
    public float SaturnTranslationVelocity;

    // Uranus
    public GameObject Uranus;
    public GameObject UranusTurningPoint;
    public float UranusRotationVelocity;
    public float UranusTranslationVelocity;

    // Neptune
    public GameObject Neptune;
    public GameObject NeptuneTurningPoint;
    public float NeptuneRotationVelocity;
    public float NeptuneTranslationVelocity;

    public TcpClient MakeSocket(string host, int port)
    {
        TcpClient sock = new TcpClient(host, port);
        return sock;
    }

    public Thread MakeThread()
    {
        try
        {
            Thread th = new Thread(new ThreadStart(this.OnSocketReadyRead));
            return th;
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
        return null;
    }

    public void AssignPlanetsProperties(string data)
    {
        string aux, txt;

        // Mercury
        aux = "mercury_rot_vel=";
        txt = data.Substring(data.IndexOf(aux) + aux.Length);
        txt = txt.Substring(0, txt.IndexOf(";"));
        this.MercuryRotationVelocity = float.Parse(txt);
        aux = "mercury_trans_vel=";
        txt = data.Substring(data.IndexOf(aux) + aux.Length);
        txt = txt.Substring(0, txt.IndexOf(";"));
        this.MercuryTranslationVelocity = float.Parse(txt);

        // Venus
        aux = "venus_rot_vel=";
        txt = data.Substring(data.IndexOf(aux) + aux.Length);
        txt = txt.Substring(0, txt.IndexOf(";"));
        this.VenusRotationVelocity = float.Parse(txt);
        aux = "venus_trans_vel=";
        txt = data.Substring(data.IndexOf(aux) + aux.Length);
        txt = txt.Substring(0, txt.IndexOf(";"));
        this.VenusTranslationVelocity = float.Parse(txt);

        // Earth
        aux = "earth_rot_vel=";
        txt = data.Substring(data.IndexOf(aux) + aux.Length);
        txt = txt.Substring(0, txt.IndexOf(";"));
        this.EarthRotationVelocity = float.Parse(txt);
        aux = "earth_trans_vel=";
        txt = data.Substring(data.IndexOf(aux) + aux.Length);
        txt = txt.Substring(0, txt.IndexOf(";"));
        this.EarthTranslationVelocity = float.Parse(txt);

        // Mars
        aux = "mars_rot_vel=";
        txt = data.Substring(data.IndexOf(aux) + aux.Length);
        txt = txt.Substring(0, txt.IndexOf(";"));
        this.MarsRotationVelocity = float.Parse(txt);
        aux = "mars_trans_vel=";
        txt = data.Substring(data.IndexOf(aux) + aux.Length);
        txt = txt.Substring(0, txt.IndexOf(";"));
        this.MarsTranslationVelocity = float.Parse(txt);

        // Jupiter
        aux = "jupiter_rot_vel=";
        txt = data.Substring(data.IndexOf(aux) + aux.Length);
        txt = txt.Substring(0, txt.IndexOf(";"));
        this.JupiterRotationVelocity = float.Parse(txt);
        aux = "jupiter_trans_vel=";
        txt = data.Substring(data.IndexOf(aux) + aux.Length);
        txt = txt.Substring(0, txt.IndexOf(";"));
        this.JupiterTranslationVelocity = float.Parse(txt);

        // Saturn
        aux = "saturn_rot_vel=";
        txt = data.Substring(data.IndexOf(aux) + aux.Length);
        txt = txt.Substring(0, txt.IndexOf(";"));
        this.SaturnRotationVelocity = float.Parse(txt);
        aux = "saturn_trans_vel=";
        txt = data.Substring(data.IndexOf(aux) + aux.Length);
        txt = txt.Substring(0, txt.IndexOf(";"));
        this.SaturnTranslationVelocity = float.Parse(txt);

        // Uranus
        aux = "uranus_rot_vel=";
        txt = data.Substring(data.IndexOf(aux) + aux.Length);
        txt = txt.Substring(0, txt.IndexOf(";"));
        this.UranusRotationVelocity = float.Parse(txt);
        aux = "uranus_trans_vel=";
        txt = data.Substring(data.IndexOf(aux) + aux.Length);
        txt = txt.Substring(0, txt.IndexOf(";"));
        this.UranusTranslationVelocity = float.Parse(txt);

        // Neptune
        aux = "neptune_rot_vel=";
        txt = data.Substring(data.IndexOf(aux) + aux.Length);
        txt = txt.Substring(0, txt.IndexOf(";"));
        this.NeptuneRotationVelocity = float.Parse(txt);
        aux = "neptune_trans_vel=";
        txt = data.Substring(data.IndexOf(aux) + aux.Length);
        txt = txt.Substring(0);
        this.NeptuneTranslationVelocity = float.Parse(txt);
    }

    public void OnSocketReadyRead()
    {
        try
        {
            // variables
            int length;
            string data;
            string message;
            byte[] incomingData;
            Byte[] bytes = new Byte[1024];
            NetworkStream stream;

            while (true)
            {
                stream = this.socket.GetStream();
                do
                {
                    length = stream.Read(bytes, 0, bytes.Length);
                    incomingData = new byte[length];
                    Array.Copy(bytes, 0, incomingData, 0, length);
                    message = Encoding.ASCII.GetString(incomingData);
                    Debug.Log("From server: " + message);

                    if (message.Contains("_to_unity_parameters_"))
                    {
                        data = message.Replace("_to_unity_parameters_(", "");
                        data = data.Replace(")", "");
                        AssignPlanetsProperties(data);
                    }
                    else if (message.Contains("_to_unity_abort_"))
                    {
                        Debug.Log("Aborted...");
                        return;
                    }
                }
                while (length != 0);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public void SendData(string msg)
    {
        try
        {
            byte[] data;
            NetworkStream stream;

            if (this.socket != null)
            {
                stream = this.socket.GetStream();
                if (stream.CanWrite)
                {
                    data = Encoding.ASCII.GetBytes(msg);
                    stream.Write(data, 0, data.Length);
                    Debug.Log("Message sent: " + msg);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public void InitPlanetsProperties()
    {
        // Mercury
        this.MercuryRotationVelocity = 1.0f;
        this.MercuryTranslationVelocity = 1.0f;

        // Venus
        this.VenusRotationVelocity = 1.0f;
        this.VenusTranslationVelocity = 1.0f;

        // Earth
        this.EarthRotationVelocity = 1.0f;
        this.EarthTranslationVelocity = 1.0f;

        // Mars
        this.MarsRotationVelocity = 1.0f;
        this.MarsTranslationVelocity = 1.0f;

        // Jupiter
        this.JupiterRotationVelocity = 1.0f;
        this.JupiterTranslationVelocity = 1.0f;

        // Saturn
        this.SaturnRotationVelocity = 1.0f;
        this.SaturnTranslationVelocity = 1.0f;

        // Uranus
        this.UranusRotationVelocity = 1.0f;
        this.UranusTranslationVelocity = 1.0f;

        // Neptune
        this.NeptuneRotationVelocity = 1.0f;
        this.NeptuneTranslationVelocity = 1.0f;
    }

    public void InitThread()
    {
        this.socket = this.MakeSocket("localhost", 12345);
        this.thread = this.MakeThread();
        this.thread.Start();
        this.SendData("Unity ready...");
    }

    public void SendPlanetsProperties()
    {
        string data = "_to_pyqt_parameters_(mercury_rot_vel={0};mercury_trans_vel={1};";
        data += "venus_rot_vel={2};venus_trans_vel={3};";
        data += "earth_rot_vel={4};earth_trans_vel={5};";
        data += "mars_rot_vel={6};mars_trans_vel={7};";
        data += "jupiter_rot_vel={8};jupiter_trans_vel={9};";
        data += "saturn_rot_vel={10};saturn_trans_vel={11};";
        data += "uranus_rot_vel={12};uranus_trans_vel={13};";
        data += "neptune_rot_vel={14};neptune_trans_vel={15})";
        data = string.Format(data, this.MercuryRotationVelocity, this.MercuryTranslationVelocity,
                             this.VenusRotationVelocity, this.VenusTranslationVelocity,
                             this.EarthRotationVelocity, this.EarthTranslationVelocity,
                             this.MarsRotationVelocity, this.MarsTranslationVelocity,
                             this.JupiterRotationVelocity, this.JupiterTranslationVelocity,
                             this.SaturnRotationVelocity, this.SaturnTranslationVelocity,
                             this.UranusRotationVelocity, this.UranusTranslationVelocity,
                             this.NeptuneRotationVelocity, this.NeptuneTranslationVelocity);
        this.SendData(data);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.InitPlanetsProperties();
        this.InitThread();
        this.SendPlanetsProperties();
    }

    // Update is called once per frame
    void Update()
    {
        // Mercury
        Mercury.transform.Rotate(0.0f, this.MercuryRotationVelocity, 0.0f, Space.Self);
        MercuryTurningPoint.transform.Rotate(0.0f, this.MercuryTranslationVelocity, 0.0f, Space.World);

        // Venus
        Venus.transform.Rotate(0.0f, this.VenusRotationVelocity, 0.0f, Space.Self);
        VenusTurningPoint.transform.Rotate(0.0f, this.VenusTranslationVelocity, 0.0f, Space.World);

        // Earth
        Earth.transform.Rotate(0.0f, this.EarthRotationVelocity, 0.0f, Space.Self);
        EarthTurningPoint.transform.Rotate(0.0f, this.EarthTranslationVelocity, 0.0f, Space.World);

        // Mars
        Mars.transform.Rotate(0.0f, this.MarsRotationVelocity, 0.0f, Space.Self);
        MarsTurningPoint.transform.Rotate(0.0f, this.MarsTranslationVelocity, 0.0f, Space.World);

        // Jupiter
        Jupiter.transform.Rotate(0.0f, this.JupiterRotationVelocity, 0.0f, Space.Self);
        JupiterTurningPoint.transform.Rotate(0.0f, this.JupiterTranslationVelocity, 0.0f, Space.World);

        // Saturn
        Saturn.transform.Rotate(0.0f, this.SaturnRotationVelocity, 0.0f, Space.Self);
        SaturnTurningPoint.transform.Rotate(0.0f, this.SaturnTranslationVelocity, 0.0f, Space.World);

        // Uranus
        Uranus.transform.Rotate(0.0f, this.UranusRotationVelocity, 0.0f, Space.Self);
        UranusTurningPoint.transform.Rotate(0.0f, this.UranusTranslationVelocity, 0.0f, Space.World);

        // Neptune
        Neptune.transform.Rotate(0.0f, this.NeptuneRotationVelocity, 0.0f, Space.Self);
        NeptuneTurningPoint.transform.Rotate(0.0f, this.NeptuneTranslationVelocity, 0.0f, Space.World);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class serialProcess  {
    private string gSerialPort = "COM1";
    private int gSerialSpeed = 9600;

    private SerialPort serailPortCommunication;

    public void serialConfig(string port, int SerialSpeed)
    {
        gSerialPort = port;
        gSerialSpeed = SerialSpeed;

        serailPortCommunication = new SerialPort(gSerialPort, gSerialSpeed, Parity.None, 8, StopBits.One);
    }

    public void setSerialConfiguration(int baudRate, int dataBits, StopBits stopBits)
    {
        serailPortCommunication.BaudRate = baudRate;
        serailPortCommunication.DataBits = dataBits;
        serailPortCommunication.StopBits = stopBits;
    }

    public void setSerialTime(int readTimeout, int writeTimeout)
    {
        serailPortCommunication.ReadTimeout = readTimeout;
        serailPortCommunication.WriteTimeout = writeTimeout;
    }

    public void serialOpen()
    {
        try
        {
            if(!serailPortCommunication.IsOpen)
            {
                serailPortCommunication.ReadTimeout = 100;
                serailPortCommunication.Open();
            }
        }
        catch
        {
            Debug.Log(serailPortCommunication.PortName + " is not Open");

        }

    }

    public SerialPort getSerialPort()
    {
        return serailPortCommunication;
    }

    public int sendSerialValue(int intValue)
    {
        byte[] result = { 0x00 };

        if (intValue == 0)
        {
            result[0] = 0x00;
        }
        else if (intValue < 15)
        {
            for (int i = 0; i < intValue; i++)
            {
                result[0] += 1;
            }
        }
        else
        {
            result[0] = 0x00;
        }

        if (serailPortCommunication.IsOpen)
        {
            serailPortCommunication.Write(result, 0, 1);
            return intValue;
        }
        else
        {
            return -1;
        }
    }

    public bool IsConnected()
    {
        if (serailPortCommunication == null)
            return false;

        return serailPortCommunication.IsOpen;
    }

    public void Close()
    {
        serailPortCommunication.Close();
    }

    public string ReadLine()
    {
        return serailPortCommunication.ReadLine();
    }

    internal void sendSerialAsciiValue(string command)
    {
        if (serailPortCommunication.IsOpen)
        {
            serailPortCommunication.Write(command);
        }
        else
        {
            return;
        }
    }

    internal string Read()
    {
        String SerialIn;
        byte[] readBuffer = new byte[serailPortCommunication.ReadBufferSize + 1];

        // If there are bytes available on the serial port,
        // Read returns up to "count" bytes, but will not block (wait)
        // for the remaining bytes. If there are no bytes available
        // on the serial port, Read will block until at least one byte
        // is available on the port, up until the ReadTimeout milliseconds
        // have elapsed, at which time a TimeoutException will be thrown.
        int count = serailPortCommunication.Read(readBuffer, 0, serailPortCommunication.ReadBufferSize);
        SerialIn = System.Text.Encoding.ASCII.GetString(readBuffer, 0, count);

        return SerialIn;
    }
}

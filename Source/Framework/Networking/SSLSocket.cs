﻿// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using System;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Threading.Tasks;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Framework.Networking
{
    public abstract class SSLSocket : ISocket, IDisposable
    {
        Socket _socket;
        internal SslStream _stream;
        IPEndPoint _remoteEndPoint;
        byte[] _receiveBuffer;

        protected SSLSocket(Socket socket)
        {
            _socket = socket;
            _remoteEndPoint = (IPEndPoint)_socket.RemoteEndPoint;
            _receiveBuffer = new byte[ushort.MaxValue];

            _stream = new SslStream(new NetworkStream(socket), false);
        }

        public virtual void Dispose()
        {
            _receiveBuffer = null;
            _stream.Dispose();
        }

        public abstract void Accept();

        public virtual bool Update()
        {
            return _socket.Connected;
        }

        public IPEndPoint GetRemoteIpEndPoint()
        {
            return _remoteEndPoint;
        }

        public async Task AsyncRead()
        {
            if (!IsOpen())
                return;

            try
            {
                var result = await _stream.ReadAsync(_receiveBuffer, 0, _receiveBuffer.Length);
                if (result == 0)
                {
                    CloseSocket();
                    return;
                }

                ReadHandler(_receiveBuffer, result);
            }
            catch (Exception ex)
            {
                Log.outException(ex);
            }
        }

        public async Task AsyncHandshake(X509Certificate2 certificate)
        {
            try
            {
                await _stream.AuthenticateAsServerAsync(certificate, false, SslProtocols.Tls12, false);
            }
            catch(Exception ex)
            {
                Log.outException(ex);
                CloseSocket();
                return;
            }

            await AsyncRead();
        }

        public abstract void ReadHandler(byte[] data, int receivedLength);

        public async Task AsyncWrite(byte[] data)
        {
            if (!IsOpen())
                return;

            try
            {
                await _stream.WriteAsync(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                Log.outException(ex);
            }
        }

        public void CloseSocket()
        {
            try
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
            }
            catch (Exception ex)
            {
                Log.outDebug(LogFilter.Network, $"WorldSocket.CloseSocket: {GetRemoteIpEndPoint()} errored when shutting down socket: {ex.Message}");
            }
        }

        public virtual void OnClose() { Dispose(); }

        public bool IsOpen() { return _socket.Connected; }

        public void SetNoDelay(bool enable)
        {
            _socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, enable);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System.Net;
using System.Threading;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Text;

namespace Chef
{

    public class AssetBundleServer 
    {
        private static HttpListener listener;
        private static string path;

        public static void StartServer()
        {
            path = Path.Combine(Application.dataPath, "World/Build/world");
            listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:9009/");
            listener.Start();
            listener.BeginGetContext(new AsyncCallback(Callback), listener);
        }

        public static void StopServer()
        {
            listener?.Stop();
            listener = null;
        }

        private static void Callback(IAsyncResult result)
        {
            if (listener.IsListening)
            {
                var ctx = listener.EndGetContext(result);

                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                resp.Headers.Add("Access-Control-Allow-Origin", "*");
                resp.ContentType = System.Net.Mime.MediaTypeNames.Application.Octet;
                resp.SendChunked = false;

                var bytes = File.ReadAllBytes(path);

                resp.ContentLength64 = bytes.Length;
                Debug.LogFormat("Bytes: {0}", bytes.Length);
                resp.StatusCode = 200;

                byte[] buffer = new byte[64 * 1024];

                resp.OutputStream.Write(bytes, 0, bytes.Length);

                Debug.Log("Finished Serving Response");
                resp.OutputStream.Close();
                resp.Close();

                listener.BeginGetContext(new AsyncCallback(Callback), listener);
            }
        }
    }
}

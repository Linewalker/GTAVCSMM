using System;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GTAV_External_Trainer.Helpers
{
    class KeysMgr
    {
        // Keys holder
        private Dictionary<int, Key> keys;

        // Update thread
        private Thread thread;
        private int interval = 20; // ms

        // Keys events
        public delegate void KeyHandler(int Id, string Name);
        public event KeyHandler KeyUpEvent;
        public event KeyHandler KeyDownEvent;

        // Key Up
        protected void OnKeyUp(int Id, string Name)
        {
            if (KeyUpEvent != null)
            {
                KeyUpEvent(Id, Name);
            }
        }

        // Key Down
        protected void OnKeyDown(int Id, string Name)
        {
            if (KeyDownEvent != null)
            {
                KeyDownEvent(Id, Name);
            }
        }

        // Init
        public KeysMgr()
        {
            keys = new Dictionary<int, Key>();
            thread = new Thread(new ParameterizedThreadStart(Update));
            thread.Start();
        }

        // Add key
        public void AddKey(int keyId, string keyName)
        {
            if (!keys.ContainsKey(keyId))
            {
                keys.Add(keyId, new Key(keyId, keyName));
            }
        }

        // Add key
        public void AddKey(System.Windows.Forms.Keys key)
        {
            int keyId = (int)key;
            if (!keys.ContainsKey(keyId))
            {
                keys.Add(keyId, new Key(keyId, key.ToString()));
            }
        }

        // Is Key Down
        public bool IsKeyDown(int keyId)
        {
            Key value;
            if (keys.TryGetValue(keyId, out value))
            {
                return value.IsKeyDown;
            }
            return false;
        }

        // Update Thread
        private void Update(object sender)
        {
            while (true)
            {
                if (keys.Count > 0)
                {
                    List<Key> keysData = new List<Key>(keys.Values);
                    if (keysData != null && keysData.Count > 0)
                    {
                        foreach (Key key in keysData)
                        {
                            if (Convert.ToBoolean(Manager.GetKeyState(key.Id) & Manager.KEY_PRESSED))
                            {
                                if (!key.IsKeyDown)
                                {
                                    key.IsKeyDown = true;
                                    OnKeyDown(key.Id, key.Name);
                                }
                            }
                            else
                            {
                                if (key.IsKeyDown)
                                {
                                    key.IsKeyDown = false;
                                    OnKeyUp(key.Id, key.Name);
                                }
                            }
                        }
                    }
                }
                Thread.Sleep(interval);
            }
        }
    }
}

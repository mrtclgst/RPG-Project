using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingSystem2 : MonoBehaviour
    {

        internal void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);

            //stream.WriteByte(0xc2);
            //stream.WriteByte(0xa1);
            //stream.WriteByte(0x48);
            //stream.WriteByte(0x4f);
            //stream.WriteByte(0x4c);
            //stream.WriteByte(0x41);
            //stream.WriteByte(0x21);

            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                //byte[] bytes = Encoding.UTF8.GetBytes("¡Hola Mundo");
                //Transform playerTransform = GetPlayerTransform();
                BinaryFormatter formatter = new BinaryFormatter();
                //SerializableVector3 position = new SerializableVector3(transform.position);
                formatter.Serialize(stream, CaptureState());

            }
        }

        internal void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Loading from " + GetPathFromSaveFile(saveFile));
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                //byte[] buffer = new byte[stream.Length];
                //stream.Read(buffer, 0, buffer.Length);
                //Encoding.UTF8.GetString(buffer);
                //Transform playerTransform = GetPlayerTransform();
                BinaryFormatter formatter = new BinaryFormatter();
                RestoreState(formatter.Deserialize(stream));
                //playerTransform.position = DeserializeVector3(position);
                //playerTransform.position = position.ToVector();
            }
        }

        private object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (SaveableEntity saveables in FindObjectsOfType<SaveableEntity>())
            {
                state[saveables.GetUniqueIdentifier()] = saveables.CaptureState();
            }
            return state;
        }

        private void RestoreState(object state)
        {
            Dictionary<string, object> stateDictionary = (Dictionary<string, object>)state;
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                saveable.RestoreState(stateDictionary[saveable.GetUniqueIdentifier()]);
            }
        }

        string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }

        //private Transform GetPlayerTransform()
        //{
        //    return GameObject.FindWithTag("Player").transform;
        //}
        //byte[] SerializeVector3(Vector3 vector)
        //{
        //    byte[] vectorBytes = new byte[3 * 4]; //bir float 4 byte 
        //    BitConverter.GetBytes(vector.x).CopyTo(vectorBytes, 0);
        //    BitConverter.GetBytes(vector.y).CopyTo(vectorBytes, 4);
        //    BitConverter.GetBytes(vector.z).CopyTo(vectorBytes, 8);
        //    return vectorBytes;
        //}
        //Vector3 DeserializeVector3(byte[] buffer)
        //{
        //    Vector3 result = new Vector3();
        //    result.x = BitConverter.ToSingle(buffer, 0); //single : float, double double
        //    result.y = BitConverter.ToSingle(buffer, 4);
        //    result.y = BitConverter.ToSingle(buffer, 8);
        //    return result;
        //}


    }
}

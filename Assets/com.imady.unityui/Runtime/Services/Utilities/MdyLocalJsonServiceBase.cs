using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using imady.NebuEvent;


namespace imady.NebuUI
{
    /// <summary>
    /// ����Unity streamingAssets�ļ�ʵ�ֵı������ݷ���
    /// </summary>
    /// <typeparam name="TClass">Ҫ��¼��ʵ����Ϣ����</typeparam>
    /// <typeparam name="TIndexType">����TClassʵ�������ı�������</typeparam>
    public abstract class MdyLocalJsonServiceBase<TClass, TIndexType> where TClass : IMdyServiceIndexable<TIndexType>
    {
        private List<TClass> Repo;
        protected List<TClass> GetAll() => Repo;

        private string FileName;

        /// <summary>
        /// import all records from the repository file (usually under Assets/StreamingAssets folder).
        /// </summary>
        /// <param name="fileName"></param>
        protected virtual NebuServiceMsg Init(string fileName)
        {
            Repo = new List<TClass>();
            FileName = fileName;

            //This will create a new respository file if not exist
            if (!File.Exists(fileName)) { UpdateRepository(Repo); }

            StreamReader streamreader = new StreamReader(FileName);
            var stream = streamreader.ReadToEnd();

            try
            {
                if (stream != null && stream.Length > 0)
                {
                    Repo.Clear();
                    Repo = JsonConvert.DeserializeObject<List<TClass>>(stream, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        Culture = new System.Globalization.CultureInfo("zh-CN")
                    });
                    //Debug.Log("[LocalJsonServiceBase]: The simulative user database was loaded successfully.");
                }
                streamreader.Close();
                return new NebuServiceMsg()
                {
                    msg = "[LocalJsonServiceBase]: The simulative repo was loaded successfully.",
                    success = true
                };
            }
            catch (Exception e)
            {
                return new NebuServiceMsg($"[LocalJsonServiceBase] Exception: {e.Message}");
            }
        }

        internal void CloseRepo()
        {
            Repo.Clear();
        }

        /// <summary>
        /// Add one user record to the repository.
        /// </summary>
        /// <param name="record"></param>
        protected virtual NebuServiceMsg Add(TClass record)
        {
            if (Repo != null)
            {
                Repo.Add(record);
                UpdateRepository(Repo);
                return new NebuServiceMsg(record);
            }
            else
                return new NebuServiceMsg("[LocalJsonServiceBase]:  Simulation Server Error - the designated repository was not initialized.");
        }

        /// <summary>
        /// !!!��ȫ����ԭ�û���Overwrite the previous user repository!
        /// ע�⣬�������ʱû�а���ԭ�û���Ϣ���ϣ������������û���������ԭ�û����ݴ�����ʧ��
        /// </summary>
        /// <param name="recordList"></param>
        private NebuServiceMsg UpdateRepository(List<TClass> recordList)
        {
            string jsonText = string.Empty;
            try
            {
                if (recordList != null && recordList.Count > 0)
                    jsonText = JsonConvert.SerializeObject(Repo.ToArray());
            }
            catch (Exception e)
            {
                return new NebuServiceMsg($"[LocalJsonServiceBase] JSON Exception: {e.Message}");
            }

            try
            {
                //Todo: better to modify the code to appending contents......
                if (File.Exists(FileName))
                {
                    FileInfo file = new FileInfo(FileName);
                    file.Delete();
                }
                //Write user information into repository file
                StreamWriter streamWriter = new StreamWriter(FileName, false);
                streamWriter.Write(jsonText);
                streamWriter.Close();
                return new NebuServiceMsg() { msg = $"[LocalJsonServiceBase]: {typeof(TClass)} ���ݿ��Ѿ����¡�", success = true };
            }
            catch (Exception e)
            {
                return new NebuServiceMsg($"[LocalJsonServiceBase] Streaming Writing Exception: {e.Message}");
            }
        }

        /// <summary>
        /// Update one anchor record in the repository.
        /// </summary>
        /// <param name="record"></param>        
        protected virtual NebuServiceMsg Update(TClass record)
        {
            try
            {
                var query = Repo.Where(a => a.objectIndex.Equals(record.objectIndex)).FirstOrDefault();
                if (query != null)
                {
                    var index = Repo.FindIndex(a => a.objectIndex.Equals(record.objectIndex));
                    Repo[index] = record;
                }
                else
                    Add(record);
                UpdateRepository(Repo);
                return new NebuServiceMsg(record);
            }
            catch (Exception e)
            {
                return new NebuServiceMsg($"[LocalJsonServiceBase]: Record update error: {e.Message}");
            }
        }

        /// <summary>
        /// Delete the designated user record.    
        /// </summary>
        /// <param name="recordId">��GUID��ʽ��¼��User Id��</param>
        protected virtual NebuServiceMsg Delete(TIndexType recordId)
        {
            try
            {
                var item = Repo.Where(a => a.objectIndex.Equals(recordId)).FirstOrDefault();
                Repo.Remove(item);
                return new NebuServiceMsg() { success = true, msg = $"[LocalJsonServiceBase]: Record {recordId} Deleted." };
            }
            catch (Exception e)
            {
                //Debug.LogException(e);
                return new NebuServiceMsg($"[LocalJsonServiceBase]: Record update error: {e.Message}");
            }
        }

        protected virtual NebuServiceMsg Delete(TClass record)
        {
            try
            {
                //prevent the situation that two different records with identical ID exist.
                var presearch = Repo.Find(a => a.objectIndex.Equals(record.objectIndex));

                if (presearch != null && record.Equals(presearch))
                {
                    Delete(record.objectIndex);
                    return new NebuServiceMsg() { success = true, msg = $"[LocalJsonServiceBase]: Record {record.objectIndex} Deleted." };
                }
                else
                {
                    return new NebuServiceMsg($"[LocalJsonServiceBase]: The data to delete has variance to record in database. Aborted.");
                }
            }
            catch (Exception e)
            {
                return new NebuServiceMsg($"[LocalJsonServiceBase]: Record update error: {e.Message}");
            }
        }

        /// <summary>
        /// Get the User instance designated by UserId (GUID).
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        protected virtual NebuServiceMsg Get(TIndexType recordId)
        {
            try
            {
                var instance = Repo.Where(a => a.objectIndex.Equals(recordId)).FirstOrDefault();
                if (instance == null)
                    return new NebuServiceMsg() { success = false, msgBody = default, msg = $"[LocalJsonServiceBase]: Record not found." };
                else
                    return new NebuServiceMsg(instance);
            }
            catch (Exception e)
            {
                return new NebuServiceMsg() { success = false, msgBody = default, msg = $"[LocalJsonServiceBase]: Record searching error: {e.Message}" };
            }
        }

    }
}
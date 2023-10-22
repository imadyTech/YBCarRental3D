using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace imady.NebuUI
{
    /// <summary>
    /// ����ʵ�ֶ����SetActive��Instantiate��Destroy�ȷ�����
    /// </summary>
    public class NbuPoolBase 
    {
        /// <summary>
        /// �������ж���
        /// </summary>
        private List<GameObject> objectPool;

        private NbuResourceLoader loader;


        #region CONSTRUCTOR & DESTRUCTOR
        public NbuPoolBase()
        {
            objectPool = new List<GameObject>();
            loader = new NbuResourceLoader();
        }


        public virtual void Dispose()
        {
            foreach (GameObject obj in objectPool)
            {
                UnityEngine.Object.Destroy(obj);
            }
            objectPool.Clear();
        }
        #endregion

        //todo: Ŀǰ��objectpool��ʵ�����ٹ���

        /// <summary>
        /// ���ݶ���õ�prefab���ƣ����ܰ�����/���ַ������Ҷ������pool���Ѿ����ڣ���ֻ��ȡ����������ʵ������
        /// </summary>
        /// <param name="objectPrefabPath">Ҫ����������prefab·�������硰/Prefabs/UITemplate/xxxx����</param>
        /// <returns></returns>
        protected virtual GameObject WakeMdyObject(string objectPrefabPath, Transform parentTransform, bool isNewInstance)
        {
            if (string.IsNullOrEmpty(objectPrefabPath))
                return null;
            var gameobject = objectPool
                .Where(o => o.name.Equals(objectPrefabPath))
                .FirstOrDefault();
            if (isNewInstance || gameobject == null)//cache missing, instantiate a new object
            {
                gameobject = CreateObject(objectPrefabPath, parentTransform);
                if (gameobject == null)
                {
                    Debug.LogWarning($"MdyObject���� '{objectPrefabPath}' ����ʧ�ܣ�");
                    return null;
                }
            }

            gameobject.SetActive(true);
            return gameobject;
        }

        /// <summary>
        /// ����ָ�����͵�MdyObject����ʵ�����������ɶ���ʵ����
        /// </summary>
        /// <typeparam name="SourceType"></typeparam>
        /// <param name="objectPrefabPath">������prefab��Դ·��</param>
        protected GameObject CreateObject(string objectPrefabPath, Transform parentTransform)
        {
            if (string.IsNullOrEmpty(objectPrefabPath)) return null;

            try
            {
                var gameobject = loader.LoadPrefab(objectPrefabPath);
                if (gameobject == null) return null;

                var go = UnityEngine.Object.Instantiate(gameobject);
                go.name = objectPrefabPath;
                if (parentTransform != null) go.transform.SetParent(parentTransform);
                objectPool.Add(go);//���
                return go;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return null;
            }
        }

        protected virtual void HibernateObject(GameObject obj)
        {
            if (objectPool.Contains(obj))
                obj.SetActive(false);
        }
    }
}
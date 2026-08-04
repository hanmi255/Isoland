using System.Collections.Generic;
using System.IO;
using Assets.Scripts.SaveLoadSystem;
using Assets.Scripts.Utilities;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts.SaveLoadSystem
{
    public class SaveLoadManager : SingletonMonoBehaviour<SaveLoadManager>
    {
        private string _jsonFloder;
        private readonly List<ISaveable> _saveableList = new();
        private Dictionary<string, GameSaveData> _saveDataDic = new();

        protected override void Awake()
        {
            base.Awake();
            _jsonFloder = Application.persistentDataPath + "/Save/";
        }

        private void OnEnable()
        {
            EventBus.NewWeekStartedEvent += OnNewWeekStarted;
        }

        private void OnDisable()
        {
            EventBus.NewWeekStartedEvent -= OnNewWeekStarted;
        }

        private void OnNewWeekStarted(int _week)
        {
            var resultPath = _jsonFloder + "data.sav";
            if (File.Exists(resultPath))
            {
                File.Delete(resultPath);
            }
        }

        public void Register(ISaveable saveable)
        {
            _saveableList.Add(saveable);
        }

        public void Save()
        {
            // 清空旧数据
            _saveDataDic.Clear();
            // 收集所有可保存对象的数据
            foreach (var saveable in _saveableList)
            {
                _saveDataDic.Add(saveable.GetType().Name, saveable.GenerateSaveData());
            }

            // 序列化为JSON
            var resultPath = _jsonFloder + "data.sav";
            var json = JsonConvert.SerializeObject(_saveDataDic, Formatting.Indented);

            // 确保目录存在否则创建新的
            if (!File.Exists(resultPath))
            {
                Directory.CreateDirectory(_jsonFloder);
            }

            // 写入文件
            File.WriteAllText(resultPath, json);
        }

        public void Load()
        {
            // 检查文件是否存在
            var resultPath = _jsonFloder + "data.sav";
            if (!File.Exists(resultPath))
            {
                return;
            }

            // 读取文件
            var stringData = File.ReadAllText(resultPath);
            // 反序列化为JSON
            _saveDataDic = JsonConvert.DeserializeObject<Dictionary<string, GameSaveData>>(stringData);

            // 恢复所有可保存对象的数据
            foreach (var saveable in _saveableList)
            {
                saveable.RestoreGameData(_saveDataDic[saveable.GetType().Name]);
            }
        }
    }
}

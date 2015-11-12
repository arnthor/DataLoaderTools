﻿using System;
using System.Collections.Generic;
using Aih.DataLoader.Tools.Models;
using Aih.DataLoader.Tools.StatusHandlers;
using Aih.DataLoader.Tools.PropertyHandlers;

namespace Aih.DataLoader.Tools
{

    public abstract class BaseDataLoader
    {

        protected Dictionary<string, string> _properties;
        protected IStatusHandler _statusHandler;

        public BaseDataLoader(IPropertyHandler propertyHandler, IStatusHandler statusHandler)
        {
            IPropertyHandler _propertyHandler = propertyHandler;

            string typeName = this.GetType().Name;
            _properties = propertyHandler.GetProperties(typeName);

            _statusHandler = statusHandler;
        }


        public abstract void Initialize();
        public abstract void LoadData();
        public abstract void TransformData();
        public abstract void SaveData();
        public abstract void CleanUp();


        public void RunDataLoader()
        {

            string currentClassName = this.GetType().Name;
            string LoadId = Guid.NewGuid().ToString();

            BatchStatus status = new BatchStatus() { BatchName = currentClassName, BatchId = LoadId, StartTime = DateTime.Now, Comment = "", Status = "Started" };




            //TODO: Make calls to the handler that updates the status async?
            _statusHandler.CreateBatchStatusRecord(status);
            //TODO: Check - this is all kind of redundant - perhaps better to skip ?

            SetStatusInit(status);
            Initialize();

            SetStatusLoad(status);
            LoadData();

            SetStatusTransform(status);
            TransformData();

            SetStatusSaving(status);
            SaveData();

            SetStatusCleanUp(status);
            CleanUp();

            SetStatusFinished(status);
        }


        private void SetStatusFinished(BatchStatus status)
        {
            status.FinishTime = DateTime.Now;
            status.Status = "Finished";
            _statusHandler.UpdateBatchStatusRecord(status);
        }

        private void SetStatusCleanUp(BatchStatus status)
        {
            status.StartCleanupTime = DateTime.Now;
            status.Status = "Cleaning up";
            _statusHandler.UpdateBatchStatusRecord(status);
        }


        private void SetStatusSaving(BatchStatus status)
        {
            status.StartSaveTime = DateTime.Now;
            status.Status = "Saving";
            _statusHandler.UpdateBatchStatusRecord(status);
        }


        private void SetStatusTransform(BatchStatus status)
        {
            status.StartTransformTime = DateTime.Now;
            status.Status = "Transforming";
            _statusHandler.UpdateBatchStatusRecord(status);
        }

        private void SetStatusLoad(BatchStatus status)
        {
            status.StartLoadTime = DateTime.Now;
            status.Status = "Loading";
            _statusHandler.UpdateBatchStatusRecord(status);
        }

        private void SetStatusInit(BatchStatus status)
        {
            status.StartInitTime = DateTime.Now;
            status.Status = "Initializing";
            _statusHandler.UpdateBatchStatusRecord(status);
        }


    }
}
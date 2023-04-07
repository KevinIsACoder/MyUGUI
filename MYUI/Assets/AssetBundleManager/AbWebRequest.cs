using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace AbAssets
{
    public class AbWebRequest : IEnumerator
    {
        public enum Status
        {
            Wait,
            Doaloading,
            Complete
        }
        public enum Restult
        {
            Default,
            Success,
            Fail,
            Cancel
        }

        public Status status { get; set; } = Status.Wait;
        public Restult result { get; set; } = Restult.Success;

        private bool isDown => status == Status.Complete;

        public object Current { get; }

        public Action<AbWebRequest> onComplete;

        public bool MoveNext()
        {
            return !isDown;
        }

        public void Reset()
        {
            
        }

        public virtual void OnStart()
        {
            
        }

        public virtual void OnUpdate()
        {
            
        }

        public virtual void OnComplete()
        {
            
        }

        public void Start()
        {
            if(status == Status.Doaloading) return;
            status = Status.Doaloading;
            OnStart();
        }
        
        public void Complete()
        {
            OnComplete();
            onComplete?.Invoke(this);
        }
    }   
}

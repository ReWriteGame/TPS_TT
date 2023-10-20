using System;
using UnityEngine;

namespace Class.Score
{
    [Serializable]
    public class Score
    {
        [SerializeField] private ScoreData data;

        public event Action<ScoreData> OnChangeData;
        public event Action<ScoreData> OnChangeDataLastValue;
        
        public event Action<float> OnChangeValue;
        public event Action<float> OnChangeValueLastValue;
        public event Action<float> OnIncreaseValue;
        public event Action<float> OnDecreaseValue;
        public event Action<float> OnCanNotIncreaseValue;
        public event Action<float> OnCanNotDecreaseValue;
        
        public event Action<Limit> OnChangeLimit;
        public event Action<Limit> OnChangeLimitLastValue;
        public event Action<float> OnChangeLimitMin;
        public event Action<float> OnChangeLimitMinLastValue;
        public event Action<float> OnIncreaseMinValue;
        public event Action<float> OnDecreaseMinValue;
        public event Action<float> OnChangeLimitMax;
        public event Action<float> OnChangeLimitMaxLastValue;
        public event Action<float> OnIncreaseMaxValue;
        public event Action<float> OnDecreaseMaxValue;
        public event Action OnReachMinValue;
        public event Action OnReachMaxValue;
        
        
        public float Value => data.Value;
        public float MinValue => data.ValueLimit.MinValue;
        public float MaxValue => data.ValueLimit.MaxValue;
        public Limit ValueLimit => data.ValueLimit;
        public ScoreData Data => data;
        
        
        public Score()
        {
            data = new ScoreData();
        }
         
        public Score(ScoreData data)
        {
            this.data = data;
        }
       
        
        public void SetData(ScoreData data)
        {
            var oldData = this.data;
            this.data = data;
            InvokeDataChangeEvents(oldData, this.data);
        }
        
        public void SetValue(float value)
        {
            SetData(new ScoreData(value, data.MinValue, data.MaxValue));
        }
        
        public void SetMinValue(float value)
        {
            SetData(new ScoreData(data.Value, value, data.MaxValue));
        }
        
        public void SetMaxValue(float value)
        {
            SetData(new ScoreData(data.Value, data.MinValue, value));
        }
      
        
        public void IncreaseValue(float value) 
        {
            if (value <= 0) return;
            var oldData = data;
            data.SetValue(data.Value + value);
            InvokeDataChangeEvents(oldData, data);

            InvokeIncreaseValueEvents(value);
        }

        public void DecreaseValue(float value)
        {
            if (value <= 0) return;
            var oldData = data;
            data.SetValue(data.Value - value);
            InvokeDataChangeEvents(oldData, data);
            
            InvokeDecreaseValueEvents(value);
        }
        
        
        public bool IncreaseValueLimited(float value)
        {
            if (value <= 0) return false;

            if (data.LengthCurrentScoreToMaximum() >= value)
            {
                IncreaseValue(value);
                return true;
            }
            else
            {
                OnCanNotIncreaseValue?.Invoke(value);
                return false; 
            }
        }

        public bool DecreaseValueLimited(float value)
        {
            if (value <= 0) return false;
           
            if (data.LengthCurrentScoreToMinimum() >= value) 
            {
                DecreaseValue(value);
                return true;
            }
            else
            {
                OnCanNotDecreaseValue?.Invoke(value);
                return false; 
            }
        }
        
           
        public void IncreaseLimitMin(float value)
        {
            if (value <= 0) return;
            var oldData = data;
            data.SetMinLimit(data.MinValue + value);
            InvokeDataChangeEvents(oldData, data);
            OnIncreaseMinValue?.Invoke(value);
        }
        
        public void DecreaseLimitMin(float value)
        {
            if (value <= 0) return;
            var oldData = data;
            data.SetMinLimit(data.MinValue - value);
            InvokeDataChangeEvents(oldData, data);
            OnDecreaseMinValue?.Invoke(value);
        }
        
        
        public void IncreaseLimitMax(float value)
        {
            if (value <= 0) return;
            var oldData = data;
            data.SetMaxLimit(data.MaxValue + value);
            InvokeDataChangeEvents(oldData, data);
            OnIncreaseMaxValue?.Invoke(value);
        }
        
        public void DecreaseLimitMax(float value)
        { 
            if (value <= 0) return;
            var oldData = data;
            data.SetMaxLimit(data.MaxValue - value);
            InvokeDataChangeEvents(oldData, data);
            OnDecreaseMaxValue?.Invoke(value);
        }
        
        
        public float GetLengthLimit()
        {
            return data.ValueLimit.GetLengthLimit();
        }
        
        
        private void InvokeIncreaseValueEvents(float increaseValue)
        {
            if (data.CheckValueIsMax())
                OnReachMaxValue?.Invoke();
            OnIncreaseValue?.Invoke(increaseValue);
        }
        
        private void InvokeDecreaseValueEvents(float decreaseValue)
        {
            if (data.CheckValueIsMin())
                OnReachMinValue?.Invoke();
            OnDecreaseValue?.Invoke(decreaseValue);
        }
        
        private void InvokeDataChangeEvents(ScoreData oldData, ScoreData currentData)
        {
            OnChangeData?.Invoke(currentData);
            OnChangeDataLastValue?.Invoke(oldData);

            if (!Mathf.Approximately(oldData.Value, currentData.Value))
            {
                OnChangeValue?.Invoke(currentData.Value);
                OnChangeValueLastValue?.Invoke(oldData.Value);
            }
            
            if (!Mathf.Approximately(oldData.ValueLimit.MinValue, currentData.ValueLimit.MinValue))
            {
                OnChangeLimitMin?.Invoke(currentData.ValueLimit.MinValue);
                OnChangeLimitMinLastValue?.Invoke(oldData.ValueLimit.MinValue);
            }
            
            if (!Mathf.Approximately(oldData.ValueLimit.MaxValue, currentData.ValueLimit.MaxValue))
            {
                OnChangeLimitMax?.Invoke(currentData.ValueLimit.MaxValue);
                OnChangeLimitMaxLastValue?.Invoke(oldData.ValueLimit.MaxValue);
            }
            
            if (!Mathf.Approximately(oldData.ValueLimit.MinValue, currentData.ValueLimit.MinValue)||
                !Mathf.Approximately(oldData.ValueLimit.MaxValue, currentData.ValueLimit.MaxValue))
            {
                OnChangeLimit?.Invoke(currentData.ValueLimit);
                OnChangeLimitLastValue?.Invoke(oldData.ValueLimit);
            }
        }
    }
}

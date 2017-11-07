using GalaSoft.MvvmLight;
using Shared.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Models;
using Shared.Interfaces;

namespace CD3_wi16b141_Vrtis.ViewModel
{
    public class ItemVm : ViewModelBase
    {
        //properties
        //properties - BaseModel
        private ItemBase baseItem; //Abstraktion, um das Model von der View zu trennen

        public int Id
        {
            get { return baseItem.Id; }
        }
        public string Name
        {
            get { return baseItem.Name; }
            set { baseItem.Name = value; RaisePropertyChanged(); }
        }
        public string Description
        {
            get { return baseItem.Description; }
            set { baseItem.Description = value; RaisePropertyChanged(); }
        }
        public string Room
        {
            get { return baseItem.Room; }
            set { baseItem.Room = value; RaisePropertyChanged(); }
        }
        public int PosX
        {
            get { return baseItem.PosX; }
            set { baseItem.PosX = value; RaisePropertyChanged(); }
        }
        public int PosY
        {
            get { return baseItem.PosY; }
            set { baseItem.PosY = value; RaisePropertyChanged(); }
        }

        //properties - Models
        //ValueType liefert eine string-Repräsentation des ValueType zurück (zB "Boolean")        
        public string ValueType
        {
            get
            {
                if (baseItem is ISensor) //is: Überprüft, ob ein Objekt mit einem bestimmten Typ kompatibel ist.
                    return (baseItem as BaseSensor).SensorValueType.Name;
                else if (baseItem is IActuator)
                    return (baseItem as BaseActuator).ActuatorValueType.Name;
                else
                    throw new NotImplementedException();
            }

        }

        //ItemType liefert den Type des BaseItems zurück (zB ISensor oder IActuator)
        public Type ItemType
        {
            get
            {
                if (baseItem is ISensor) return typeof(ISensor); //Name = "ISensor" FullName = "Shared.Interfaces.ISensor"
                else if (baseItem is IActuator) return typeof(IActuator); //Name = "IActuator" FullName = "Shared.Interfaces.IActuator"
                else throw new NotImplementedException();
            }
        }

        //Mode: get liefert eine string-Repräsentation vom SensorMode bzw. ActuatorMode zurück (zB Shared.Models.MotionDedector)
        //Mode: set setzt den SensorModeType bzw. den ModeType unter Berücksichtigung der Groß- und Kleinschreibung (false)
        public string Mode
        {
            get
            {
                if (baseItem is ISensor) return (baseItem as BaseSensor).SensorMode.ToString();
                if (baseItem is IActuator) return (baseItem as BaseActuator).ActuatorMode.ToString();
                else return null;
            }
            set
            {
                if (baseItem is ISensor)
                    (baseItem as BaseSensor).SensorMode = (SensorModeType)Enum.Parse(typeof(SensorModeType), value, false);
                if (baseItem is IActuator)
                    (baseItem as BaseActuator).ActuatorMode = (ModeType)Enum.Parse(typeof(ModeType), value, false);

                RaisePropertyChanged();
            }
        }

        //Value: get liefert den SensorValue vom BaseSensor bzw. den ActuatorValue vom BaseActuator zurück (zB 0, 1)
        //Value: set setzt den SensorValue vom BaseSensor bzw. den ActuatorValue vom BaseActuator
        public object Value
        {
            get
            {
                if (baseItem is ISensor)
                    return (baseItem as BaseSensor).SensorValue;
                else if (baseItem is IActuator)
                    return (baseItem as BaseActuator).ActuatorValue;
                else
                    throw new NotImplementedException();
            }

            set
            {
                if (baseItem is ISensor) (baseItem as BaseSensor).SensorValue = value;
                else if (baseItem is IActuator) (baseItem as BaseActuator).ActuatorValue = value;
                else
                    throw new NotImplementedException();
                RaisePropertyChanged();

            }
        }

        //constructor
        public ItemVm(ISensor sensor)
        {
            baseItem = sensor as ItemBase;
        }

        public ItemVm(IActuator actuator)
        {
            baseItem = actuator as ItemBase;
        }
    }
}

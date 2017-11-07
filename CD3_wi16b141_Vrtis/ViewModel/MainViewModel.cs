using CD3_wi16b141_Vrtis.DataSimulation;
using GalaSoft.MvvmLight;
using Shared.BaseModels;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace CD3_wi16b141_Vrtis.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        //properties
        //Update time and date
        private string currentTime = DateTime.Now.ToLocalTime().ToShortTimeString();
        private string currentDate = DateTime.Now.ToLocalTime().ToShortDateString();
        DispatcherTimer timer = new DispatcherTimer();
                
        public string CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; RaisePropertyChanged(); }
        }
        public string CurrentTime
        {
            get { return currentTime; }
            set { currentTime = value; RaisePropertyChanged(); }
        }

        //Actors and Sensors
        public ObservableCollection<ItemVm> ActorList { get; set; }
        public ObservableCollection<ItemVm> SensorList { get; set; }

        //Actors and Sensors - Mode
        public ObservableCollection<string> ModeSelectionList { get; private set; }

        //Actors and Sensors - Simulator
        private Simulator simulator;
        private List<ItemVm> modelItems = new List<ItemVm>();

        //constructor
        public MainViewModel()
        {
            //Update time and date
            timer.Interval = new TimeSpan(0, 0, 30); //update every 30 seconds
            timer.Tick += UpdateTime; //calls method UpdateTime

            if (!IsInDesignMode) //Methode von ViewModelBase
            {
                timer.Start(); //starts the timer
            }
            

            //Actors and Sensors
            ActorList = new ObservableCollection<ItemVm>();
            SensorList = new ObservableCollection<ItemVm>();

            //Actors and Sensors - Mode
            /* wird noch nicht benötigt
            ModeSelectionList = new ObservableCollection<string>();

            foreach (var item in Enum.GetNames(typeof(SensorModeType)))
            {
                ModeSelectionList.Add(item); //all enum types of the Shared BaseModels.SensorModeType are added
            }
            foreach (var item in Enum.GetNames(typeof(ModeType))) //all enum types of the Shared BaseModels.ModeType are added
            {
                ModeSelectionList.Add(item);

            }
            */

            //Actors and Sensors - Loading Data
            if (!IsInDesignMode)
            {
                LoadData();
            }
        }

        //methods
        //Update time and date
        private void UpdateTime(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToLocalTime().ToShortTimeString();
            CurrentDate = DateTime.Now.ToLocalTime().ToShortDateString();
        }

        //Actors and Sensors
        private void LoadData()
        {
            simulator = new Simulator(modelItems); //modelItems-Liste wird an Simulator übergeben
            foreach (var item in simulator.Items) //enthält die generierten Demo-Daten aus dem Simulator 
            {
                if (item.ItemType.Equals(typeof(ISensor)))
                    SensorList.Add(item);
                else if (item.ItemType.Equals(typeof(IActuator)))
                    ActorList.Add(item);
            }
        }

        

    }
}
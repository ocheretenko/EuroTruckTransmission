using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EuroTruckTransmission
{
    internal static class Emulation
    {

        public static void Init()
        {
            client = new ViGEmClient();

            controller = client.CreateXbox360Controller();
            controller.Connect();
        }

        static ViGEmClient client;
        static IXbox360Controller controller;

        private static int _transmission = 0;

        public static bool RealisticTransmission = false;
        public static bool RealisticTransmissionPermission = false;
        public static bool ExtraAxisEnabled = false;
        private static byte _rtAxisValue = 0;
        private static byte _ltAxisValue = 0;
        
        public static bool RbAxisMode = false;
        private static byte _rbAxisValue = 0;
        public static byte RbAxisValue
        {
            get { return _rbAxisValue; }
            set 
            { 
                if (_rbAxisValue != value)
                {
                    _rbAxisValue = value;
                    //controller.SetAxisValue(Xbox360Axis.LeftThumbX, value);
                    controller.SetSliderValue(Xbox360Slider.RightTrigger, value);
                }
            }
        }

        public static byte RTAxisValue
        {
            get { return _rtAxisValue; }
            set 
            {
                if (!ExtraAxisEnabled) return;

                if (_rtAxisValue != value)
                {
                    _rtAxisValue = value;

                    controller.SetSliderValue(Xbox360Slider.RightTrigger, value);
                }
            }
        }
        public static byte LTAxisValue
        {
            get { return _ltAxisValue; }
            set
            {
                if (!ExtraAxisEnabled) return;

                if (_ltAxisValue != value)
                {
                    _ltAxisValue = value;

                    controller.SetSliderValue(Xbox360Slider.LeftTrigger, value);
                }
            }
        }

        public static int Transmission
        {
            get{ return _transmission; }
            set {
                if (RealisticTransmission)
                    if (!RealisticTransmissionPermission) return;

                if (value > 12) return;
                if (value < -4) return;

                _transmission = value;
                Program.mainForm.labelTRANSMISSION.Text = Transmission.ToString();

                transmission_press_emulated();

                void transmission_press_emulated()
                {
                    controller.SetButtonState(Xbox360Button.Up, false);
                    controller.SetButtonState(Xbox360Button.Right, false);
                    controller.SetButtonState(Xbox360Button.Down, false);
                    controller.SetButtonState(Xbox360Button.Left, false);
                    controller.SetButtonState(Xbox360Button.LeftShoulder, false);
                    controller.SetButtonState(Xbox360Button.RightShoulder, false);
                    controller.SetButtonState(Xbox360Button.X, false);
                    Mode1 = false;
                    Mode2 = false;
                    int t = Transmission;

                    if (Transmission > 6)
                    {
                        t = Transmission - 6;
                        Mode1 = true;
                    }

                    switch (t)
                    {
                        case -1: controller.SetButtonState(Xbox360Button.X, true); break;
                        case -2: controller.SetButtonState(Xbox360Button.X, true); Mode2 = true; break;
                        case -3: controller.SetButtonState(Xbox360Button.X, true); Mode1 = true; break;
                        case -4: controller.SetButtonState(Xbox360Button.X, true); Mode1 = true; Mode2 = true; break;
                        case 1: controller.SetButtonState(Xbox360Button.Up, true); break;
                        case 2: controller.SetButtonState(Xbox360Button.Right, true); break;
                        case 3: controller.SetButtonState(Xbox360Button.Down, true); break;
                        case 4: controller.SetButtonState(Xbox360Button.Left, true); break;
                        case 5: controller.SetButtonState(Xbox360Button.LeftShoulder, true); break;
                        case 6: controller.SetButtonState(Xbox360Button.RightShoulder, true); break;
                    }
                
                }
            }
        }

        private static bool _mode1 = false;
        private static bool _mode2 = false;

        public static bool Mode1
        {
            get { return _mode1; }
            set { 

                if (value != _mode1)
                {
                    controller.SetButtonState(Xbox360Button.Y, value);

                    if (value)
                        Program.mainForm.labelMode1.BackColor = System.Drawing.Color.Red;
                    else
                        Program.mainForm.labelMode1.BackColor = System.Drawing.Color.White;

                    _mode1 = value;
                }
            }
        }
        public static bool Mode2
        {
            get { return _mode2; }
            set
            {

                if (value != _mode2)
                {
                    controller.SetButtonState(Xbox360Button.A, value);

                    if (value)
                        Program.mainForm.labelMode2.BackColor = System.Drawing.Color.Red;
                    else
                        Program.mainForm.labelMode2.BackColor = System.Drawing.Color.White;

                    _mode2 = value;
                }
            }
        }

        public static void CloseAndExit()
        {
            controller.Disconnect();
        }

        public static void TransmissionClick(int t)
        {
            var rt = RealisticTransmission;
            RealisticTransmission = false;
            
            Transmission = 0;
            Transmission = t;
            Transmission = 0;

            RealisticTransmission = rt;
        }

        public static void Mode1Click()
        {
            Mode1 = true;
            Mode1 = false;
        }

        public static void Mode2Click()
        {
            Mode2 = true;
            Mode2 = false;
        }
    }
}

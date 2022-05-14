using SnagFree.TrayApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EuroTruckTransmission
{
    public static class KeyboardListening
    {
        private static GlobalKeyboardHook _globalKeyboardHook;

        public static Keys TransmissionUpKey = Keys.E;
        public static Keys TransmissionDownKey = Keys.F;

        public static void Init()
        {
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
        }

        public static void CloseAndExit()
        {
            _globalKeyboardHook?.Dispose();
        }

        private static void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            //Debug.WriteLine(e.KeyboardData.VirtualCode);

            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp)
            {
                if (e.KeyboardData.VirtualCode == (int)TransmissionUpKey)
                {
                    Emulation.Transmission++;
                }

                if (e.KeyboardData.VirtualCode == (int)TransmissionDownKey)
                {
                    Emulation.Transmission--;
                }
            }

            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {

                switch (e.KeyboardData.VirtualCode)
                {
                    case (int)Keys.NumPad9: Emulation.TransmissionClick(-1); break;
                    case (int)Keys.NumPad1: Emulation.TransmissionClick(1); break;
                    case (int)Keys.NumPad2: Emulation.TransmissionClick(2); break;
                    case (int)Keys.NumPad3: Emulation.TransmissionClick(3); break;
                    case (int)Keys.NumPad4: Emulation.TransmissionClick(4); break;
                    case (int)Keys.NumPad5: Emulation.TransmissionClick(5); break;
                    case (int)Keys.NumPad6: Emulation.TransmissionClick(6); break;
                    case (int)Keys.NumPad7: Emulation.Mode1Click(); break;
                    case (int)Keys.NumPad8: Emulation.Mode2Click(); break;
                    case (int)Keys.Multiply: Emulation.RTAxisValue = 200; Emulation.RTAxisValue = 0; break;
                    case (int)Keys.Divide: Emulation.LTAxisValue = 200; Emulation.LTAxisValue = 0; break;
                    case (int)Keys.NumPad0: Emulation.RbAxisValue = 200; Emulation.RbAxisValue = 0; break;
                }
            }
        }

    }
}

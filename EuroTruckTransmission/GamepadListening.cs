using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XInput.Wrapper;

namespace EuroTruckTransmission
{
    public static class GamepadListening
    {
        private static X.Gamepad gamepad = null;
        private static int gamepadNumber = 1;
        private static bool xstate = false, ystate = false, bstate = false, astate = false;

        static GamepadListening()
        {

        }

        public static string SelectNextController()
        {
            gamepadNumber++;
            if (gamepadNumber > 4) gamepadNumber = 1;

            return Connect();
        }
        public static string SelectPreviousController()
        {
            gamepadNumber--;
            if (gamepadNumber < 1) gamepadNumber = 4;

            return Connect();
        }

        public static string Connect()
        {
            try
            {
                if (gamepad != null) X.StopPolling();

                if (X.IsAvailable)
                {

                    switch (gamepadNumber)
                    {
                        case 1: gamepad = X.Gamepad_1; break;
                        case 2: gamepad = X.Gamepad_2; break;
                        case 3: gamepad = X.Gamepad_3; break;
                        case 4: gamepad = X.Gamepad_4; break;
                    }

                    //gamepad.KeyDown += Gamepad_KeyDown; ;
                    gamepad.StateChanged += Gamepad_StateChanged;

                    X.StartPolling(gamepad);


                    return gamepadNumber.ToString() + ": CONNECTED";
                }

                else
                {
                    return "CONNECTION ERROR";
                }
            }

            catch
            {
                return "CONNECTION ERROR";
            }
        }

        private static void Gamepad_StateChanged(object sender, EventArgs e)
        {
            if (gamepad.A_down)
                Program.mainForm.buttonA.BackColor = Color.Red;
            else
                Program.mainForm.buttonA.BackColor = Color.White;

            if (gamepad.B_down)
                Program.mainForm.buttonB.BackColor = Color.Red;
            else
                Program.mainForm.buttonB.BackColor = Color.White;

            if (gamepad.X_down)
                Program.mainForm.buttonX.BackColor = Color.Red;
            else
                Program.mainForm.buttonX.BackColor = Color.White;


            if (gamepad.Y_down)
                Program.mainForm.buttonY.BackColor = Color.Red;
            else
                Program.mainForm.buttonY.BackColor = Color.White;


            if (gamepad.Y_down)
            {
                if (ystate == false)
                {
                    Emulation.Transmission++;
                    ystate = true;
                }
            }

            if (gamepad.Y_up)
            {
                if (ystate == true)
                {
                    ystate = false;
                }
            }

            if (gamepad.B_down)
            {
                if (bstate == false)
                {
                    Emulation.Transmission--;
                    bstate = true;
                }
            }

            if (gamepad.B_up)
            {
                if (bstate == true)
                {
                    bstate = false;
                }
            }

            if (gamepad.X_down)
            {
                Emulation.Transmission = 0;
            }

            if (gamepad.LTrigger > 240) Emulation.RealisticTransmissionPermission = true;
            else Emulation.RealisticTransmissionPermission = false;


            if (Emulation.ExtraAxisEnabled)
            {
                if (gamepad.A_down)
                {
                    Emulation.LTAxisValue = (byte)gamepad.RTrigger;
                    Emulation.RTAxisValue = 0;
                }
                else
                {
                    Emulation.RTAxisValue = (byte)gamepad.RTrigger;
                    Emulation.LTAxisValue = 0;
                }
            }

            if (Emulation.RbAxisMode)
            {
                if (gamepad.RBumper_down)
                {
                    Emulation.RbAxisValue = 255;
                }
                else Emulation.RbAxisValue = 0;
            }
        }

        internal static void CloseAndExit()
        {
            try
            {
                X.StopPolling();
            }
            catch
            {
                return;
            }
        }
    }
}

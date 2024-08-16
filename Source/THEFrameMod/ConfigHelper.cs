using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace THEFrameMod
{
    internal class ConfigHelper
    {
        public static ConfigEntry<Vector3> position;
         
        public static ConfigEntry<Quaternion> rotation;

        public static void ConstructConfigFile()
        {
            position = Main.config.Bind("Frame",      // The section under which the option is shown
                             "Positon",  // The key of the configuration option in the configuration file
                             new Vector3(-62.8f, 12.6f, -83.7f), // The default value
                             "Position of the frame!"); // Description of the option to show in the config file

            rotation = Main.config.Bind("Frame",
                                                "Rotation",
                                                Quaternion.Euler(0, 277, 0),
                                                "Rosition of the frame!");
        }

        public static void UpdatePosition(Vector3 newPosition, Quaternion newRotation)
        {
            position.Value = newPosition;
            rotation.Value = newRotation;
        }
    }
}

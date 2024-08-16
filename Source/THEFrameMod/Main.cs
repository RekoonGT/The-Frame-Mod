using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using THEFrameMod.Patches;
using UnityEngine;

namespace THEFrameMod
{
    [BepInPlugin(Constants.ModGUID, Constants.ModName, Constants.ModVersion)]
    public class Main : BaseUnityPlugin
    {
        public static ConfigFile config;
        void Awake()
        {
            config = this.Config;
            ConfigHelper.ConstructConfigFile();
            new Harmony(Constants.ModGUID).PatchAll(Assembly.GetExecutingAssembly());

            OnPlayerSpawned.OnSpawned += delegate
            {
                if (File.Exists(Constants.ImageName))
                {
                    using (Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("THEFrameMod.Frame.theframe"))
                    {
                        var bundle = AssetBundle.LoadFromStream(str);

                        GameObject Frame = Instantiate(bundle.LoadAsset<GameObject>(Constants.FramesAssetName));
                        Material imageMaterial = imageMaterial = bundle.LoadAsset<Material>(Constants.ImageMaterialname);
                        bundle.LoadAsset<Material>(Constants.WoodMaterialname);
                        //Console.WriteLine("Loaded Asset!");

                        Frame.transform.localPosition = ConfigHelper.position.Value;
                        Frame.transform.localScale = new Vector3(50, -50, 5);
                        Frame.transform.localRotation = ConfigHelper.rotation.Value;
                        Frame.layer = 8;
                        Frame.AddComponent<Holdable>();
                        //Console.WriteLine("Moved Frame!");

                        Texture2D image = ReturnTextureFromPath(Constants.ImageName);
                        imageMaterial.mainTexture = image;
                        //Console.WriteLine("Loaded Image!");
                    }
                }
                else
                {
                    Console.WriteLine("Make sure to follow the steps in the ReadMe!");
                }
            };
        }

        public Texture2D ReturnTextureFromPath(string path)
        {
            Texture2D texture = new Texture2D(1, 1);
            byte[] thebytesof87 = File.ReadAllBytes(path);
            texture.LoadImage(thebytesof87);
            texture.filterMode = FilterMode.Point;
            texture.Apply();
            return texture;
        }
    }
}
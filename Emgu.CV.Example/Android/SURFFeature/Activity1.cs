﻿using System;
using System.IO;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;

using Emgu.CV;
using Emgu.CV.Structure;

namespace SURFFeatureExample
{
   [Activity(Label = "SURF Feature", MainLauncher = true, Icon = "@drawable/icon")]
   public class Activity1 : Activity
   {
      protected override void OnCreate(Bundle bundle)
      {
         base.OnCreate(bundle);

         // Set our view from the "main" layout resource
         SetContentView(Resource.Layout.Main);

         // Get our button from the layout resource,
         // and attach an event to it
         Button button = FindViewById<Button>(Resource.Id.MatchButton);
         ImageView imageView = FindViewById<ImageView>(Resource.Id.ResultImageView);
         TextView messageView = FindViewById<TextView>(Resource.Id.MessageView);

         button.Click += delegate 
         {
            long time;
            using (Image<Gray, Byte> box = new Image<Gray,byte>(Assets, "box.png"))
            using (Image<Gray, Byte> boxInScene = new Image<Gray, byte>(Assets, "box_in_scene.png"))
            using (Image<Bgr, Byte> result = DrawMatches.Draw(box, boxInScene, out time))
            {
               messageView.Text = String.Format("Matched in {0} milliseconds.", time);
               imageView.SetImageBitmap( result.ToBitmap() );
            }
         };
      }
   }
}

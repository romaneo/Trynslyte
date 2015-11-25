﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Translyte.Android;
using Translyte.Core.ViewModels;
using Object = Java.Lang.Object;

namespace Translyte.Android.CustomClasses
{
    class GalleryAdapter : BaseAdapter<BookViewModel>
    {
        private List<BookViewModel> _items;
        private Context _context;

        public GalleryAdapter(Context context, List<BookViewModel> items)
        {
            _items = items;
            _context = context;
        }

        public override BookViewModel this[int position]
        {
            get { return _items[position]; }
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView ?? LayoutInflater.From(_context).Inflate(Resource.Layout.BookItemView, null, false);

            ImageView image = row.FindViewById<ImageView>(Resource.Id.Cover);
            if (_items[position].Cover != null)
            {
                var img = GetCover(_items[position].Cover);
                image.SetImageBitmap(img);
            }
            

            TextView title = row.FindViewById<TextView>(Resource.Id.Title);
            title.Text = _items[position].Title;

            TextView author = row.FindViewById<TextView>(Resource.Id.Author);
            author.Text = _items[position].Author;

            CheckBox checkBox = row.FindViewById<CheckBox>(Resource.Id.IsAvailable);
            checkBox.Checked = _items[position].IsAvailable;

            return row;
        }

        private Bitmap GetCover(string imageText)
        {
            Byte[] bitmapData = Convert.FromBase64String(FixBase64ForImage(imageText));
            var imageBitmap = BitmapFactory.DecodeByteArray(bitmapData, 0, bitmapData.Length);
            return imageBitmap;
        }


        public string FixBase64ForImage(string Image)
        {
            System.Text.StringBuilder sbText = new System.Text.StringBuilder(Image, Image.Length);
            sbText.Replace("\r\n", String.Empty); sbText.Replace(" ", String.Empty);
            return sbText.ToString();
        }

        public override int GetItemViewType(int position)
        {
            return position; //(position == this.Count - 1) ? 1 : 0;
        }

        public override int ViewTypeCount
        {
            get { return this.Count; }
        }
    }
}
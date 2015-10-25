﻿using Cirrious.MvvmCross.ViewModels;
using Translyte.Core.Models;
using System;
using Cirrious.MvvmCross.Plugins.File;

namespace Translyte.Core.ViewModels
{
    public class BookViewModel 
		: MvxViewModel
    {
        public BookViewModel()
        {
            Path = "/sdcard/translyte/gg.fb2";
            _book = BookReader.Load(Path);
            _title = _book.Chapters[1].Title;
            _content = _book.Chapters[1].Content;
            
        }
        //private readonly IMvxFileStore _fs;
        private BookModel _book;
        public string Path { get; set; }
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }
        private string _content="Book's content";
        public string Content { 
            get { return _content; } 
            set { 
                _content = value; 
                RaisePropertyChanged(()=>Content); 
            } 
        }
    }
}
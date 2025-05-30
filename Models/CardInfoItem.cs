﻿namespace shiennymendeline.github.io.Models
{
    public class CardInfoItem
    {        
        public string ImgPath { get; set; }        
        public string Title { get; set; }        
        public string Caption { get; set; }
        public string[] Tags { get; set; } = Array.Empty<string>();
        public List<LinkInfo> Links { get; set; }
    }
}

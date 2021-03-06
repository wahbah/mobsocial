﻿using System;
using mobSocial.Core.Data;
using mobSocial.Data.Enum;
using mobSocial.Data.Interfaces;

namespace mobSocial.Data.Entity.MediaEntities
{
    public class Media : BaseEntity, IUserResource
    {
        public string Name { get; set; }

        public string SystemName { get; set; }

        public string Description { get; set; }

        public string AlternativeText { get; set; }

        public string LocalPath { get; set; }

        public string ThumbnailPath { get; set; }

        public string MimeType { get; set; }

        public byte[] Binary { get; set; }

        public MediaType MediaType { get; set; }

        public int UserId { get; set; }

        public DateTime DateCreated { get; set; }
    }

    public class MediaMap: BaseEntityConfiguration<Media> { }
}
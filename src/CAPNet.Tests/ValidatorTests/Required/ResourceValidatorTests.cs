﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;
using CAPNet.Tests;
using CAPNet.Models;

namespace CAPNet
{
    public class ResourceValidatorTests
    {
        [Fact]
        public void ResourceWithDescriptionAndMimeTypeIsValid()
        {
            var info = CreateInfoWithResourceWithDescriptionAndMimeType("Description", "MimeType");

            var resourceValidator = new ResourceValidator(info);
            Assert.True(resourceValidator.IsValid);
            Assert.Equal(0, resourceValidator.Errors.Count());
        }

        [Fact]
        public void ResourceWithNoMimeTypeIsInvalid()
        {
            var alert = CreateInfoWithResourceWithDescriptionAndMimeType("Description", null);

            var resourceValidator = new ResourceValidator(alert);
            Assert.False(resourceValidator.IsValid);
            Assert.Equal(1, resourceValidator.Errors.Count());
        }

        [Fact]
        public void ResourceWithoutMimeTypeAndDescriptionHasTwoErrors()
        {
            var alert = CreateInfoWithResourceWithDescriptionAndMimeType(null, null);

            var resourceValidator = new ResourceValidator(alert);
            Assert.False(resourceValidator.IsValid);
            Assert.Equal(2, resourceValidator.Errors.Count());
        }

        private static Info CreateInfoWithResourceWithDescriptionAndMimeType(string description, string mimeType)
        {
            var info = new Info();
            var resource = new Resource();
            resource.Description = description;
            resource.MimeType = mimeType;
            info.Resources.Add(resource);

            return info;
        }
    }
}

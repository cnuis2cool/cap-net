﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CAPNet.Models;

namespace CAPNet
{
    /// <summary>
    /// 
    /// </summary>
    public class ResourceValidator : Validator<Info>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public ResourceValidator(Info info) : base(info) { }

        /// <summary>
        /// 
        /// </summary>
        public override bool IsValid
        {
            get
            {
                return !Errors.Any();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override IEnumerable<Error> Errors
        {
            get
            {
                return from resource in Entity.Resources
                       from error in GetErrors(resource)
                       select error;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        private IEnumerable<Error> GetErrors(Resource resource)
        {
            var resourceValidators = new List<Validator<Resource>>();

            resourceValidators.Add(new MimeTypeRequiredValidator(resource));
            resourceValidators.Add(new ResourceDescriptionRequiredValidator(resource));

            return from validator in resourceValidators
                   from error in validator.Errors
                   select error;
        }
    }
}

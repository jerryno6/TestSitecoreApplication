﻿using events.tac.local.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAC.Sitecore.Abstractions.Interfaces;
using TAC.Sitecore.Abstractions.SitecoreImplementation;

namespace events.tac.local.Business
{
    public class BreadcrumbRedBuilder
    {
        private IRenderingContext _context;

        public BreadcrumbRedBuilder() : this(SitecoreRenderingContext.Create())
        {

        }

        public BreadcrumbRedBuilder(IRenderingContext renderingContext)
        {
            this._context = renderingContext;
        }

        public IEnumerable<BreadcrumRedItem> Build()
        {
            var ancestors = _context.ContextItem.GetAncestors().ToList();
            var b = _context.ContextItem;

            return _context?.HomeItem == null || _context?.ContextItem == null ?
                Enumerable.Empty<BreadcrumRedItem>() :
                _context
                    .ContextItem
                    .GetAncestors()
                    .Where(i => _context.HomeItem.IsAncestorOrSelf(i))
                    .Select(i => new BreadcrumRedItem
                    (
                        title: i.DisplayName,
                        url: i.Url,
                        active: false
                    ))
                    .Concat(new[]
                    {
                        new BreadcrumRedItem
                        (
                            title: _context.ContextItem.DisplayName,
                            url: _context.ContextItem.Url,
                            active:true
                        )
                    });
        }
    }
}
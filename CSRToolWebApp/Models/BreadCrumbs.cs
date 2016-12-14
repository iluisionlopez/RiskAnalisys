using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSRToolWebApp.Models
{
    public class BreadCrumbs
    {
        public List<BreadCrumb> Items { get; set; }

        public BreadCrumbs()
        {
            this.Items = new List<BreadCrumb>();
        }

        public void Add(BreadCrumb breadCrumb)
        {
            this.Items.Add(breadCrumb);
        }
        public BreadCrumb Add(string text)
        {
            return Add(text, "", false);
        }
        public BreadCrumb Add(string text, string url)
        {
            return Add(text, url, false);
        }
        public BreadCrumb Add(string text, bool selected)
        {
            return Add(text, "", selected);
        }
        public BreadCrumb Add(string text, string url, bool selected)
        {
            BreadCrumb breadCrumb = new BreadCrumb(text, url, selected);
            this.Items.Add(breadCrumb);
            return breadCrumb;
        }
    }

    public class BreadCrumb
    {
        public string Text { get; set; }
        public string Url { get; set; }
        public bool Selected { get; set; }
        public string ShowMessageOnClick { get; set; }

        public BreadCrumb(string text)
        {
            this.Text = text;
            this.Selected = false;
        }
        public BreadCrumb(string text, string url)
        {
            this.Text = text;
            this.Url = url;
            this.Selected = false;
        }
        public BreadCrumb(string text, bool selected)
        {
            this.Text = text;
            this.Selected = selected;
        }
        public BreadCrumb(string text, string url, bool selected)
        {
            this.Text = text;
            this.Url = url;
            this.Selected = selected;
        }
        public BreadCrumb(string text, string url, string showMessageOnClick)
        {
            this.Text = text;
            this.Url = url;
            this.Selected = false;
            this.ShowMessageOnClick = showMessageOnClick;
        }
        public BreadCrumb(string text, string url, string showMessageOnClick, bool selected)
        {
            this.Text = text;
            this.Url = url;
            this.Selected = selected;
            this.ShowMessageOnClick = showMessageOnClick;
        }
    }
}
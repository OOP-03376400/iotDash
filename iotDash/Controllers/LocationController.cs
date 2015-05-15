﻿using iotDash.Models;
using iotDash.Service;
using iotDbConnector.DAL;
using iotServiceProvider;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iotDash.Controllers
{
	[DomainAuthorize]
	public class LocationController : Controller
	{
		//
		// GET: /Location/
		public ActionResult Index()
		{
			IiotDomainService cl = new iotServiceConnector().GetDomainClient();
			List<Location> locs = cl.Locations().ToList();
			LocationListViewModel model = new LocationListViewModel(locs);
			return View(model);
		}

		public string New(string Name, string Lat, string Lng)
		{
			try
			{
				IiotDomainService cl = new iotServiceConnector().GetDomainClient();
				Location loc = new Location();
				loc.LocationName = Name;
				loc.Lng = double.Parse(Lng, CultureInfo.InvariantCulture);
				loc.Lat = double.Parse(Lat, CultureInfo.InvariantCulture);
				cl.LocationAdd(loc);
				return "Location added sucessfully";
			}
			catch (Exception e)
			{
				return "Location add failed";
			}
		}


		public ActionResult Add()
		{
			return View();
		}
		public ActionResult Edit(string LocationId)
		{
			try
			{
				int LocId = int.Parse(LocationId);
				IiotDomainService cl = new iotServiceConnector().GetDomainClient();
				Location loc = cl.LocationWithId(LocId);
				if (loc != null)
				{
					LocationEditViewModel model = new LocationEditViewModel(loc);
					return View(model);
				}     
			}
			catch (Exception e)
			{
			}
			return View();
		}

		public bool Remove(string LocationId)
		{
			try
			{
				int LocId = int.Parse(LocationId);
				IiotDomainService cl = new iotServiceConnector().GetDomainClient();
				Location loc = cl.LocationWithId(LocId);
				//TODO remove
				return true;     
			}
			catch (Exception e)
			{
				return false;
			}      
		}


	}
}
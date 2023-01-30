﻿
using BulkyBookPro.DataAccess;
using BulkyBookPro.DataAccess.Repository.IRepository;
using BulkyBookPro.Models;
using BulkyBookPro.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyBookWebPro.Areas.Admin.Controllers;
//[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
        return View(objCoverTypeList);
    }
  


    //GET
    public IActionResult Upsert(int? id)
    {
        ProductVM productVM = new()
        {
            Product = new(),
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
            CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),

        };

        if (id == null || id == 0)
        {
            //create product
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
            return View(productVM);
        }
        else
        {
            //update product
        }
        
        return View(productVM);
    }
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(CoverType obj)
    {

        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType edited successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var coverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

        if (coverTypeFromDbFirst == null)
        {
            return NotFound();
        }

        return View(coverTypeFromDbFirst);
    }
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
        if (obj == null)
        {
            return NotFound();
        }
        _unitOfWork.CoverType.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "CoverType deleted successfully";
        return RedirectToAction("Index");
    }
}

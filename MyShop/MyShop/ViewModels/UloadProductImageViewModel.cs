﻿using MyShop.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyShop.ViewModels
{
    public class UloadProductImageViewModel : NotifyModel
    {
        List<MediaFile> ProductImages;
        public INavigation Navigation { get; set; }
        public UloadProductImageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            ProductImages = new List<MediaFile>();
        }

        public Command CaptureImage
        {
            get
            {
                return new Command(async (e) =>
                {
                    int index = Convert.ToInt32(e);
                    var actionSheet = await Application.Current.MainPage.DisplayActionSheet("Title", "Cancel", "", "Take Photo", "Choose from Gallary");
                    if (actionSheet == "Take Photo")
                    {
                        CaptureImages(index, 1);
                    }
                    else
                    {
                        CaptureImages(index, 2);
                    }
                });
            }
        }

        public async void CaptureImages(int index, int type)
        {

            if (await GlobalFunctions.GetCameraPermission() && await GlobalFunctions.GetStorageReadPermission() && await GlobalFunctions.GetStorageWritePermission())
            {
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }
                MediaFile file = null;
                if (type == 1)
                {
                    file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = Guid.NewGuid().ToString() + ".jpeg"//default name or random no using guid
                    });
                }
                else
                {
                    file = await CrossMedia.Current.PickPhotoAsync();
                }

                if (file != null)
                {
                    switch (index)
                    {
                        case 1:
                            ImageUrl1 = ImageSource.FromStream(() =>
                            {
                                var stream = file.GetStream();
                                return stream;
                            });
                            break;
                        case 2:
                            ImageUrl2 = ImageSource.FromStream(() =>
                            {
                                var stream = file.GetStream();
                                return stream;
                            });
                            break;
                        case 3:
                            ImageUrl3 = ImageSource.FromStream(() =>
                            {
                                var stream = file.GetStream();
                                return stream;
                            });
                            break;
                        case 4:
                            ImageUrl4 = ImageSource.FromStream(() =>
                            {
                                var stream = file.GetStream();
                                return stream;
                            });
                            break;
                        case 5:
                            ImageUrl5 = ImageSource.FromStream(() =>
                            {
                                var stream = file.GetStream();
                                return stream;
                            });
                            break;
                    }


                    ProductImages.Add(file);
                    // MessagingCenter.Send<MediaFile>(file, "ProductImage");
                }
            }
        }


        ImageSource _imageUrl1;
        public ImageSource ImageUrl1
        {
            get { return _imageUrl1; }
            set
            {
                if (value != null)
                {
                    _imageUrl1 = value;
                    OnPropertyChanged();
                }
            }
        }
        ImageSource _imageUrl2;
        public ImageSource ImageUrl2
        {
            get { return _imageUrl2; }
            set
            {
                if (value != null)
                {
                    _imageUrl2 = value;
                    OnPropertyChanged();
                }
            }
        }
        ImageSource _imageUrl3;
        public ImageSource ImageUrl3
        {
            get { return _imageUrl3; }
            set
            {
                if (value != null)
                {
                    _imageUrl3 = value;
                    OnPropertyChanged();
                }
            }
        }
        ImageSource _imageUrl4;
        public ImageSource ImageUrl4
        {
            get { return _imageUrl4; }
            set
            {
                if (value != null)
                {
                    _imageUrl4 = value;
                    OnPropertyChanged();
                }
            }
        }
        ImageSource _imageUrl5;
        public ImageSource ImageUrl5
        {
            get { return _imageUrl5; }
            set
            {
                if (value != null)
                {
                    _imageUrl5 = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}

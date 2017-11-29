using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using System;
using Android.Gms.Maps.Model;

namespace gmaps_tutorial
{
	[Activity(Label = "gmaps_tutorial", MainLauncher = true)]
	public class MainActivity : Activity, IOnMapReadyCallback
	{
		private GoogleMap mMap;

		private Button btnNormal, btnHybrid, btnSatellite, btnTerrain;

		public void OnMapReady(GoogleMap googleMap)
		{
			mMap = googleMap;

			LatLng latlng = new LatLng(40.776408, -73.97075); //New York
			LatLng latlng2 = new LatLng(41, -73);
			CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latlng, 10);
			mMap.MoveCamera(camera);

			MarkerOptions options = new MarkerOptions()
			.SetPosition(latlng)
			.SetTitle("New York")
			.SetSnippet("AKA: The Big Apple")
			.Draggable(true);

			mMap.AddMarker(options); //Marker 1

			//Marker 2
			mMap.AddMarker(new MarkerOptions()
				.SetPosition(latlng2)
				.SetTitle("Marker 2")
				.Draggable(true)
				.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueBlue)));

			mMap.MarkerClick += MMap_MarkerClick;
			mMap.MarkerDragEnd += MMap_MarkerDragEnd;
		}

		private void MMap_MarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
		{
			LatLng pos = e.Marker.Position;
			mMap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(pos, 10));
		}

		private void MMap_MarkerDragEnd(object sender, GoogleMap.MarkerDragEndEventArgs e)
		{
			LatLng pos = e.Marker.Position;
			Console.WriteLine(pos.ToString());
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);
			btnNormal = FindViewById<Button>(Resource.Id.btnNormal);
			btnHybrid = FindViewById<Button>(Resource.Id.btnHybrid);
			btnSatellite = FindViewById<Button>(Resource.Id.btnSatellite);
			btnTerrain = FindViewById<Button>(Resource.Id.btnTerrain);

			btnNormal.Click += btnNormal_Click;
			btnHybrid.Click += btnHybridl_Click;
			btnSatellite.Click += btnSatellite_Click;
			btnTerrain.Click += btnTerrain_Click;

			SetUpMap();
		}

		void btnTerrain_Click(object sender, EventArgs e)
		{
			mMap.MapType = GoogleMap.MapTypeTerrain;
		}

		void btnSatellite_Click(object sender, EventArgs e)
		{
			mMap.MapType = GoogleMap.MapTypeSatellite;
		}

		void btnHybridl_Click(object sender, EventArgs e)
		{
			mMap.MapType = GoogleMap.MapTypeHybrid;
		}

		void btnNormal_Click(object sender, EventArgs e)
		{
			mMap.MapType = GoogleMap.MapTypeNormal;
		}

		private void SetUpMap()
		{
			if (mMap == null)
			{
				FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
			}
		}
	}
}


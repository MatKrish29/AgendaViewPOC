using AgendaViewPOC.RecyclerViewHelper;
using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.Content;
using AndroidX.Core.Graphics.Drawable;
using Google.Android.Material.FloatingActionButton;
using System.Collections.Generic;
using System.Linq;

namespace AgendaViewPOC
{
    [Activity(Label = "AgendaViewPOC", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private List<ItemData> _itemData = new List<ItemData>();
        private FloatingActionButton fab;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
/*            fab.Click += Fab_Click;*/

            InitializeData();

            /*TextView header = FindViewById<TextView>(Resource.Id.sticky_header);*/
            RecyclerView recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerAgendaView);
            recyclerView.HasFixedSize = true;
            LinearLayoutManager layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.SetAdapter(new RecyclerViewAdapter(_itemData, recyclerView));
        }

        private void Fab_Click(object sender, System.EventArgs e)
        {
            fab.SetImageResource(Resource.Drawable.ic_calendar);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void InitializeData()
        {
            _itemData.Add(new ItemData() { MainText = "Jason Roy", SubText = "1990" });
            _itemData.Add(new ItemData() { MainText = "Aaron Finch", SubText = "1993" });
            _itemData.Add(new ItemData() { MainText = "Tom Holland", SubText = "1960" });
            _itemData.Add(new ItemData() { MainText = "Henry Cavill", SubText = "1992" });
            _itemData.Add(new ItemData() { MainText = "Robert Pattinson", SubText = "1998" });
            _itemData.Add(new ItemData() { MainText = "Cillian Murphy", SubText = "1995" });
            _itemData.Add(new ItemData() { MainText = "Eoin Morgan", SubText = "1996" });
            _itemData.Add(new ItemData() { MainText = "David Warner", SubText = "1994" });
            _itemData.Add(new ItemData() { MainText = "Steve Smith", SubText = "1991" });
            _itemData.Add(new ItemData() { MainText = "Tony Stark", SubText = "1999" });
            _itemData.Add(new ItemData() { MainText = "Bruce Wayne", SubText = "2001" });
            _itemData.Add(new ItemData() { MainText = "Jos Buttler", SubText = "1988" });
            _itemData.Add(new ItemData() { MainText = "James Anderson", SubText = "1943" });
            _itemData.Add(new ItemData() { MainText = "Chris Heimsworth", SubText = "1970" });
            _itemData.Add(new ItemData() { MainText = "Jon Snow", SubText = "1984" });
            _itemData.Add(new ItemData() { MainText = "Jon Bernthal", SubText = "1980" });
            _itemData.Add(new ItemData() { MainText = "Tiger Woods", SubText = "2001" });

            _itemData = _itemData.OrderBy(data => data.MainText).ToList();
        }

        protected override bool OnPrepareOptionsPanel(View view, IMenu menu)
        {
            MenuInflater inflater = MenuInflater;
            inflater.Inflate(Resource.Layout.example_menu, menu);

            SearchView searchView = new SearchView(this);
            searchView.SetImeOptions(ImeAction.Done);

            EditText editText = (EditText)searchView.FindViewById(Resource.Id.search_src_text);
            editText?.SetTextColor(Color.White);
            editText?.SetHintTextColor(Color.White);
            editText?.SetHint(Resource.String.hint);

            ImageView closeButton = (ImageView)searchView.FindViewById(Resource.Id.search_close_btn);
            closeButton?.SetColorFilter(Color.White);

            IMenuItem searchItem = menu.FindItem(Resource.Id.action_search);
            searchItem.SetActionView(searchView);

            Drawable drawable = searchItem.Icon;
            drawable = DrawableCompat.Wrap(drawable);
            DrawableCompat.SetTint(drawable, Color.White);
            searchItem.SetIcon(drawable);

            return base.OnPrepareOptionsPanel(view, menu);
        }
    }
}
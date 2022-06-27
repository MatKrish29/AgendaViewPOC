using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace AgendaViewPOC.RecyclerViewHelper
{
    public class RecyclerViewAdapter : RecyclerView.Adapter
    {
        public event EventHandler<RecyclerViewAdapterClickEventArgs> ItemClick;
        public event EventHandler<RecyclerViewAdapterClickEventArgs> ItemLongClick;

        private readonly List<ItemData> _viewItems;
        private readonly string _sampleHeader = "Section Header - ";
        private readonly TextView _header;
        private readonly RecyclerView _recyclerView;

        public RecyclerViewAdapter(List<ItemData> data, RecyclerView recyclerView)
        {
            _viewItems = data;
/*            _header = header;*/ 
            _recyclerView = recyclerView;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            int id = Resource.Layout.custom_view;
            View itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);
            RecyclerViewAdapterViewHolder viewHolder = new RecyclerViewAdapterViewHolder(itemView, OnClick, OnLongClick);
            return viewHolder;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ItemData item = _viewItems[position];
            ItemData previousItem = position == 0 ? null : _viewItems[position-1];
            ItemData nextItem = position == ItemCount - 1 ? null : _viewItems[position + 1];
            RecyclerViewAdapterViewHolder viewHolder = holder as RecyclerViewAdapterViewHolder;

/*            LinearLayoutManager layoutManager = ((LinearLayout)mRecyclerView.getLayoutManager());
            int firstVisiblePosition = layoutManager.findFirstVisibleItemPosition();*/

            viewHolder.MainText.Text = item.MainText;
            viewHolder.SubText.Text = item.SubText;
            viewHolder.TimeIndicator.Visibility = ViewStates.Gone;
            viewHolder.CardSpace.SetBackgroundColor(Color.Red);

/*            if (item.SubText == "2001")
            {
                viewHolder.TimeIndicator.Visibility = ViewStates.Visible;
            }*/

            if (previousItem == null)
            {
                viewHolder.HeaderSectionText.Text = _sampleHeader + item.MainText.Substring(0, 1);
                viewHolder.SectionText.Text = item.MainText.Substring(1, 1).ToUpper();
                viewHolder.SectionText.Visibility = ViewStates.Visible;
                viewHolder.HeaderSectionText.Visibility = ViewStates.Visible;
            }
            else
            {
                if (item.MainText.Substring(0, 1) == previousItem.MainText.Substring(0, 1) && item.MainText.Substring(1, 1) == previousItem.MainText.Substring(1, 1))
                {
                    viewHolder.SectionText.Visibility = ViewStates.Invisible;
                    viewHolder.HeaderSectionText.Visibility = ViewStates.Gone;
                }
                else if (item.MainText.Substring(0, 1) == previousItem.MainText.Substring(0, 1) && item.MainText.Substring(1, 1) != previousItem.MainText.Substring(1, 1))
                {
                    viewHolder.SectionText.Text = item.MainText.Substring(1, 1).ToUpper();
                    viewHolder.SectionText.Visibility = ViewStates.Visible;
                    viewHolder.HeaderSectionText.Visibility = ViewStates.Gone;
                }
                else
                {
                    viewHolder.SectionText.Text = item.MainText.Substring(1, 1).ToUpper();
                    viewHolder.SectionText.Visibility = ViewStates.Visible;
                    viewHolder.HeaderSectionText.Text = _sampleHeader + item.MainText.Substring(0, 1);
                    viewHolder.HeaderSectionText.Visibility = ViewStates.Visible;
                }
            }

            if (nextItem != null)
            {
                if (item.MainText.Substring(1, 1) != nextItem.MainText.Substring(1, 1) && item.MainText.Substring(0, 1) == nextItem.MainText.Substring(0, 1))
                {
                    viewHolder.CardSpace.Visibility = ViewStates.Visible;
                }
                else
                {
                    viewHolder.CardSpace.Visibility = ViewStates.Gone;
                }
            }
            else
            {
                viewHolder.CardSpace.Visibility = ViewStates.Gone;
                viewHolder.HeaderSectionText.Text = _sampleHeader + item.MainText.Substring(0, 1);
                viewHolder.SectionText.Text = item.MainText.Substring(1, 1).ToUpper();
            }
        }

        public override int ItemCount => _viewItems.Count;

        void OnClick(RecyclerViewAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(RecyclerViewAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class RecyclerViewAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView MainText { get; set; }

        public TextView SubText { get; set; }

        public TextView SectionText { get; set; }

        public TextView HeaderSectionText { get; set; }

        public TextView TimeIndicator { get; set; }

        public Space CardSpace { get; set; }

        public RecyclerViewAdapterViewHolder(View itemView, Action<RecyclerViewAdapterClickEventArgs> clickListener, Action<RecyclerViewAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            MainText = itemView.FindViewById<TextView>(Resource.Id.main_text);
            SubText = itemView.FindViewById<TextView>(Resource.Id.sub_text);
            SectionText = itemView.FindViewById<TextView>(Resource.Id.section_text);
            HeaderSectionText = itemView.FindViewById<TextView>(Resource.Id.section_header);
            TimeIndicator = itemView.FindViewById<TextView>(Resource.Id.time_indicator);
            CardSpace = itemView.FindViewById<Space>(Resource.Id.card_space);

            itemView.Click += (sender, e) => clickListener(new RecyclerViewAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new RecyclerViewAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class RecyclerViewAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}
﻿using Microsoft.AspNetCore.Components;
using MudBlazor.Extensions;

namespace MudBlazor
{
    public partial class MudRangeInput<T> : MudBaseInput<Range<T>>
    {
        private string _textStart, _textEnd;

        public MudRangeInput()
        {
            _value = new Range<T>();
            Converter = new RangeConverter<T>();
        }

        protected string Classname => MudInputCssHelper.GetClassname(this,
            () => !string.IsNullOrEmpty(Text) || Adornment == Adornment.Start || !string.IsNullOrWhiteSpace(PlaceholderStart) || !string.IsNullOrWhiteSpace(PlaceholderEnd));

        protected string InputClassname => MudInputCssHelper.GetInputClassname(this);

        protected string AdornmentClassname => MudInputCssHelper.GetAdornmentClassname(this);
                
        /// <summary>
        /// The short hint displayed in the start input before the user enters a value.
        /// </summary>
        [Parameter] public string PlaceholderStart { get; set; }

        /// <summary>
        /// The short hint displayed in the end input before the user enters a value.
        /// </summary>
        [Parameter] public string PlaceholderEnd { get; set; }

        protected string InputTypeString => InputType.ToDescriptionString();

        /// <summary>
        /// ChildContent of the MudInput will only be displayed if InputType.Hidden and if its not null.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        public string TextStart
        {
            get => _textStart;
            set
            {
                if (_textStart == value)
                    return;
                _textStart = value;
                Text = RangeConverter<T>.Join(_textStart, _textEnd);
            }
        }

        public string TextEnd
        {
            get => _textEnd;
            set
            {
                if (_textEnd == value)
                    return;
                _textEnd = value;
                Text = RangeConverter<T>.Join(_textStart, _textEnd);
            }
        }

        protected override void UpdateTextProperty(bool updateValue)
        {
            base.UpdateTextProperty(updateValue);

            RangeConverter<T>.Split(Text, out _textStart, out _textEnd);
        }

        protected override void UpdateValueProperty(bool updateText)
        {
            base.UpdateValueProperty(updateText);

            RangeConverter<T>.Split(Text, out _textStart, out _textEnd);
        }
    }
}
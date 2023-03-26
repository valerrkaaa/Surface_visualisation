using System;
using System.Windows.Forms;

namespace Lab1Gluschenko
{
    public class TrackBarWithTextBox
    {
        private readonly TrackBar _trackBar;
        private readonly TextBox _textBox;
        private int _lastCorrectValue;
        private int _previousValue;
        private readonly int id;

        private readonly Action _refreshPictureBox;  // для вызова функции перерисовки холста
        private readonly Action<string, int, int> _addToHistory;  // для вызова функции логирования действий пользователя

        // для предотвращения цикличного вызова функций valueUpdate
        private bool _canUpdateTrackBar = true;
        private bool _canUpdateTextBox = true;

        public TrackBarWithTextBox(TrackBar trackBar, TextBox textBox, Action refreshPictureBox, Action<string, int, int> addToHistory, int id)
        {
            this.id = id;

            _trackBar = trackBar;
            _textBox = textBox;

            _refreshPictureBox = refreshPictureBox;
            _addToHistory = addToHistory;

            _lastCorrectValue = trackBar.Value;
            _previousValue = _lastCorrectValue;

            _textBox.Text = _lastCorrectValue.ToString();

            _trackBar.ValueChanged += TrackBarValueChanged;
            _trackBar.MouseUp += TrackBarMouseUp;
            _textBox.TextChanged += TextBoxTextChanged;
        }


        private void TrackBarMouseUp(object sender, EventArgs e)
        {
            _addToHistory.Invoke("tbtb", id, _previousValue);
            _previousValue = _lastCorrectValue;
        }

        private void TrackBarValueChanged(object sender, EventArgs e)
        {
            // При изменении вручную, меняет значение у текстбокса

            if (_canUpdateTrackBar)
            {
                _canUpdateTextBox = false;
                _lastCorrectValue = _trackBar.Value;
                _textBox.Text = _lastCorrectValue.ToString();
                _refreshPictureBox.Invoke();
            }
            else
                _canUpdateTrackBar = true;
        }

        private void TextBoxTextChanged(object sender, EventArgs e)
        {
            // При изменении вручную, меняет значение у трекбара

            if (_canUpdateTextBox)
            {
                _canUpdateTrackBar = false;
                _lastCorrectValue = GetCorrectValue(_textBox.Text);
                _trackBar.Value = _lastCorrectValue;
                _textBox.Text = _lastCorrectValue.ToString();
                _refreshPictureBox.Invoke();
                _addToHistory.Invoke("tbtb", id, _previousValue);
                _previousValue = _lastCorrectValue;
            }
            else
                _canUpdateTextBox = true;
        }

        private int GetCorrectValue(string rawText)
        {
            // Возвращает текущее значение

            if (int.TryParse(rawText, out int correctValue))
                return Math.Min(_trackBar.Maximum, Math.Max(_trackBar.Minimum, correctValue));
            else
                return _lastCorrectValue;
        }

        public void SetValue(int newValue)
        {
            // сеттер

            _canUpdateTextBox = false;
            _canUpdateTrackBar = false;
            _lastCorrectValue = Math.Min(_trackBar.Maximum, Math.Max(_trackBar.Minimum, newValue));
            _previousValue = _lastCorrectValue;
            _trackBar.Value = _lastCorrectValue;
            _textBox.Text = _lastCorrectValue.ToString();
        }

        public int GetValue()
        {
            // геттер

            return _lastCorrectValue;
        }

        public int GetMinValue()
        {
            return _trackBar.Minimum;
        }

        public int GetMaxValue()
        {
            return _trackBar.Maximum;
        }
    }
}

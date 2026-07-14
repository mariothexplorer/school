using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentACar
{
    public partial class Form3 : Form
    {
        public Form3(RentACarDBEntities r)
        {
            InitializeComponent();
            this.db = r;
            Load += Form1_Load;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // Wire location change for reservations panel so we can filter cars by location
            comboBox9.SelectedIndexChanged += comboBox9_SelectedIndexChanged;
            // Wire date picker events to manage previous value and validation
            dateTimePicker2.Enter += dateTimePicker2_Enter;
            dateTimePicker2.ValueChanged += dateTimePicker2_ValueChanged;
            // Also wire the rent date picker so invalid changes are reverted
            dateTimePicker1.Enter += dateTimePicker1_Enter;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
        }
        RentACarDBEntities db = new RentACarDBEntities();

        int selectedId = -1;            // за панел 1
        int selectedCarId = -1;         // за панел 2 (Cars)
        int selectedClientId = -1;      // за панел 3 (Customers)
        int selectedReservationId = -1; // за панел 4 (Reservations)

        // store last valid return and rent dates so we can revert when user picks invalid value
        private DateTime lastValidReturnDate = DateTime.Today;
        private DateTime lastValidRentDate = DateTime.Today;

        // When true, date pickers' validation/messages are suppressed (used during programmatic population)
        private bool suppressDateValidation = false;


        // ======================================================
        // FORM LOAD
        // ======================================================
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = new[]
            {
                "Марки",
                "Категории",
                "Горива",
                "Скоростни кутии",
                "Локации",
                "Клиенти",
                "Резервации",
                "Коли"
            };
            comboBox1.SelectedIndex = -1;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false; // Panel 3 hidden at start
            panel4.Visible = false; // Panel 4 hidden at start (reservations)
            LoadCarComboBoxes();
            dataGridView1.DataSource = null;
        }

        // ======================================================
        // LOAD CAR COMBOBOXES (panel 2)
        // ======================================================
        private void LoadCarComboBoxes()
        {
            comboBox2.DataSource = db.Categories.ToList();
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "ID";

            comboBox3.DataSource = db.Brands.ToList();
            comboBox3.DisplayMember = "Name";
            comboBox3.ValueMember = "ID";

            comboBox4.DataSource = db.Locations.ToList();
            comboBox4.DisplayMember = "Name";
            comboBox4.ValueMember = "ID";

            comboBox5.DataSource = db.Fuels.ToList();
            comboBox5.DisplayMember = "Name";
            comboBox5.ValueMember = "ID";

            comboBox6.DataSource = db.Gearboxes.ToList();
            comboBox6.DisplayMember = "Name";
            comboBox6.ValueMember = "ID";
        }

        // ======================================================
        // LOAD RESERVATION COMBOBOXES (panel 4) - updated to include cars (comboBox8)
        // Changed: comboBox8 initially left empty. comboBox9 remains populated.
        // ======================================================
        private void LoadReservationComboBoxes()
        {
            // Populate comboBox7 with customers ordered alphabetically by FullName
            var customers = db.Customers
                .OrderBy(c => c.FullName)
                .Select(c => new { c.ID, c.FullName })
                .ToList();
            comboBox7.DataSource = customers;
            comboBox7.DisplayMember = "FullName";
            comboBox7.ValueMember = "ID";
            comboBox7.SelectedIndex = -1;

            // Leave comboBox8 (cars) empty initially — user will choose a location first (comboBox9)
            comboBox8.DataSource = null;
            comboBox8.Items.Clear();
            comboBox8.DisplayMember = "Display";
            comboBox8.ValueMember = "ID";
            comboBox8.SelectedIndex = -1;

            // Populate comboBox9 with locations ordered alphabetically by Name
            var locations = db.Locations
                .OrderBy(l => l.Name)
                .Select(l => new { l.ID, l.Name })
                .ToList();
            comboBox9.DataSource = locations;
            comboBox9.DisplayMember = "Name";
            comboBox9.ValueMember = "ID";
            comboBox9.SelectedIndex = -1;
        }

        // ======================================================
        // COMBOBOX1 CHANGE
        // ======================================================
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearReservationPanel();
            selectedId = -1;
            selectedCarId = -1;
            selectedClientId = -1;
            selectedReservationId = -1;

            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
            textBox1.Clear();
            if (comboBox1.SelectedItem == null)
                return;


            if (comboBox1.Text == "Коли")
            {
                // Refresh car-related comboboxes from the database each time the user chooses "Коли"
                LoadCarComboBoxes();

                dataGridView1.DataSource = db.Cars.Select(c => new
                {
                    c.ID,
                    c.VIN,
                    Brand = c.Brand.Name,
                    Category = c.Category.Name,
                    Fuel = c.Fuel.Name,
                    Gearbox = c.Gearbox.Name,
                    Location = c.Location.Name,
                    c.Model,
                    c.YearOfProduction,
                    c.Odometer
                }).ToList();
                panel2.Visible = true;
                panel1.Visible = false;
                panel3.Visible = false;
                panel4.Visible = false;

                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox4.Clear();
                textBox5.Clear();
                comboBox2.SelectedIndex = -1;

                comboBox3.SelectedIndex = -1;
                comboBox4.SelectedIndex = -1;
                comboBox5.SelectedIndex = -1;
                comboBox6.SelectedIndex = -1;

                return;
            }

            if (comboBox1.Text == "Клиенти")
            {
                // Show panel 3 and load Customers
                panel3.Visible = true;
                panel1.Visible = false;
                panel2.Visible = false;
                panel4.Visible = false;

                // Clear client textboxes (6,7,8)
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();

                // Show selected columns from the Customers table.
                dataGridView1.DataSource = db.Customers.Select(c => new
                {
                    c.ID,
                    c.FullName,
                    c.PhoneNumber,
                    c.LicenseNumber
                }).ToList();

                dataGridView1.ClearSelection();
                return;
            }

            if (comboBox1.Text == "Резервации")
            {
                // Show panel 4 and load Reservations (like Form1)
                panel4.Visible = true;
                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = false;

                // Clear reservation controls
                LoadReservationComboBoxes();
                dateTimePicker1.Value = DateTime.Today;
                dateTimePicker2.Value = DateTime.Today;
                // update last valid return/rent dates after programmatic change
                lastValidReturnDate = dateTimePicker2.Value;
                lastValidRentDate = dateTimePicker1.Value;
                textBox11.Clear();

                // Load reservations into grid with ID so we can select record
                dataGridView1.DataSource = db.Reservations
                    .AsEnumerable()
                    .Select(r => new
                    {
                        r.CustomerId,
                        Customer = r.Customer.FullName,
                        r.CarId,
                        Car = r.Car.Model,
                        Location = r.Car.Location.Name,
                        r.FromDate,
                        r.ToDate,
                        Amount = RoundUpToTwoDecimals(r.Amount)
                    })
                    .ToList();

                dataGridView1.ClearSelection();
                return;
            }

            // Default: panel1 for other single-column tables
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;

            switch (comboBox1.SelectedItem.ToString())
            {
                case "Марки":
                    dataGridView1.DataSource = db.Brands.Select(x => new { x.ID, x.Name }).ToList();
                    break;
                case "Категории":
                    dataGridView1.DataSource = db.Categories.Select(x => new { x.ID, x.Name }).ToList();
                    break;
                case "Горива":
                    dataGridView1.DataSource = db.Fuels.Select(x => new { x.ID, x.Name }).ToList();
                    break;
                case "Скоростни кутии":
                    dataGridView1.DataSource = db.Gearboxes.Select(x => new { x.ID, x.Name }).ToList();
                    break;
                case "Локации":
                    dataGridView1.DataSource = db.Locations.Select(x => new { x.ID, x.Name }).ToList();
                    break;
            }

            dataGridView1.ClearSelection();
        }

        // ======================================================
        // GRID SELECTION
        // ======================================================
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            if (comboBox1.SelectedItem != null && comboBox1.Text == "Коли")
            {
                selectedCarId = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
                var car = db.Cars.Find(selectedCarId);

                textBox2.Text = car.VIN;
                textBox3.Text = car.Model;
                textBox4.Text = car.Odometer.ToString();
                textBox5.Text = car.YearOfProduction.ToString();

                comboBox2.SelectedValue = car.CategoryId;
                comboBox3.SelectedValue = car.BrandId;
                comboBox4.SelectedValue = car.LocationId;
                comboBox5.SelectedValue = car.FuelId;
                comboBox6.SelectedValue = car.GearboxId;
                return;
            }

            if (comboBox1.SelectedItem != null && comboBox1.Text == "Клиенти")
            {
                // When a client row is selected, fill textBox6/7/8 with customer's data.
                selectedClientId = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
                var client = db.Customers.Find(selectedClientId);
                if (client == null) return;

                textBox6.Text = GetFirstPropertyValue(client, new[] { "FullName" });
                textBox7.Text = GetFirstPropertyValue(client, new[] { "PhoneNumber" });
                textBox8.Text = GetFirstPropertyValue(client, new[] { "LicenseNumber" });

                return;
            }

            if (comboBox1.SelectedItem != null && comboBox1.Text == "Резервации")
            {
                // Use composite key (CustomerId, CarId, FromDate) — read values from the selected row
                var row = dataGridView1.SelectedRows[0];

                var custObj = GetCellValueFromRow(row, "CustomerId", "Customer");
                var carObj = GetCellValueFromRow(row, "CarId", "Car");
                var fromObj = GetCellValueFromRow(row, "FromDate", "From");

                if (custObj == null || carObj == null || fromObj == null) return;

                int customerId;
                int carId;
                DateTime fromDate;
                try
                {
                    customerId = Convert.ToInt32(custObj);
                    carId = Convert.ToInt32(carObj);
                    fromDate = Convert.ToDateTime(fromObj).Date;
                }
                catch
                {
                    return;
                }

                // Query by composite key. Use AsEnumerable to compare Date component locally.
                var reservation = db.Reservations
                    .Where(r => r.CustomerId == customerId && r.CarId == carId)
                    .AsEnumerable()
                    .FirstOrDefault(r => r.FromDate.Date == fromDate);

                if (reservation == null) return;

                // Ensure reservation comboboxes are populated (comboBox8 left empty until location chosen)
                LoadReservationComboBoxes();

                try
                {
                    comboBox7.SelectedValue = reservation.CustomerId;
                }
                catch
                {
                    comboBox7.SelectedIndex = -1;
                }

                // Use the Cars table to get the car's LocationId reliably.
                var car = db.Cars.Find(reservation.CarId);
                if (car != null)
                {
                    // Suppress validation/messages while we populate pickers and combo boxes programmatically.
                    suppressDateValidation = true;
                    try
                    {
                        // Set date pickers first so comboBox9 handler can rely on the values.
                        dateTimePicker1.Value = reservation.FromDate;
                        dateTimePicker2.Value = reservation.ToDate;

                        // update last valid return/rent dates after programmatic change
                        lastValidReturnDate = dateTimePicker2.Value;
                        lastValidRentDate = dateTimePicker1.Value;

                        // Now set location — this will trigger comboBox9_SelectedIndexChanged, but because
                        // suppressDateValidation is true it won't show the "select date" message and will
                        // populate comboBox8 based on the current picker values.
                        try
                        {
                            comboBox9.SelectedValue = car.LocationId;
                        }
                        catch
                        {
                            comboBox9.SelectedIndex = -1;
                        }

                        // Now set the car in comboBox8 (comboBox8 should be populated by the location change handler)
                        try
                        {
                            comboBox8.SelectedValue = reservation.CarId;
                        }
                        catch
                        {
                            comboBox8.SelectedIndex = -1;
                        }
                    }
                    finally
                    {
                        suppressDateValidation = false;
                    }
                }
                else
                {
                    comboBox9.SelectedIndex = -1;
                    comboBox8.SelectedIndex = -1;
                }

                // Show the amount rounded up to 2 decimals in the panel
                textBox11.Text = RoundUpToTwoDecimals(reservation.Amount).ToString("F2");

                return;
            }

            selectedId = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
            textBox1.Text = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
        }

        // Helper: return first non-null string value from object's properties
        private string GetFirstPropertyValue(object obj, string[] propertyNames)
        {
            if (obj == null) return string.Empty;
            var t = obj.GetType();
            foreach (var name in propertyNames)
            {
                var p = t.GetProperty(name);
                if (p == null) continue;
                var v = p.GetValue(obj);
                if (v != null)
                    return v.ToString();
            }
            return string.Empty;
        }

        // Helper: try several possible column identifiers and return the first non-null cell value
        private object GetCellValueFromRow(DataGridViewRow row, params string[] possibleNames)
        {
            if (row == null) return null;

            // 1) Try direct access by column name (works when binding anonymous objects / property names)
            foreach (var name in possibleNames)
            {
                try
                {
                    var cell = row.Cells[name];
                    if (cell != null && cell.Value != null)
                        return cell.Value;
                }
                catch
                {
                    // ignore and continue
                }
            }

            // 2) Fallback: search by column HeaderText or Name
            foreach (DataGridViewCell cell in row.Cells)
            {
                var col = cell.OwningColumn;
                if (col == null) continue;
                foreach (var name in possibleNames)
                {
                    if (string.Equals(col.HeaderText, name, StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(col.Name, name, StringComparison.OrdinalIgnoreCase))
                    {
                        if (cell.Value != null) return cell.Value;
                    }
                }
            }

            return null;
        }

        // Helper: round up to 2 decimal places (ceiling)
        private decimal RoundUpToTwoDecimals(decimal value)
        {
            return Math.Ceiling(value * 100M) / 100M;
        }

        // ======================================================
        // BUTTON 1 – ADD (ОРИГИНАЛНИЯТ ТИ КОД)
        // ======================================================
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Избери таблица.");
                return;
            }

            string name = textBox1.Text.Trim();
            if (name == "")
            {
                MessageBox.Show("Въведи име.");
                return;
            }

            switch (comboBox1.SelectedItem.ToString())
            {
                case "Марки":
                    if (db.Brands.Any(x => x.Name == name))
                    {
                        MessageBox.Show("Такъв запис съществува.");
                        return;
                    }
                    db.Brands.Add(new Brand { Name = name });
                    break;

                case "Категории":
                    if (db.Categories.Any(x => x.Name == name))
                    {
                        MessageBox.Show("Такъв запис съществува.");
                        return;
                    }
                    db.Categories.Add(new Category { Name = name });
                    break;

                case "Горива":
                    if (db.Fuels.Any(x => x.Name == name))
                    {
                        MessageBox.Show("Такъв запис съществува.");
                        return;
                    }
                    db.Fuels.Add(new Fuel { Name = name });
                    break;

                case "Скоростни кутии":
                    if (db.Gearboxes.Any(x => x.Name == name))
                    {
                        MessageBox.Show("Такъв запис съществува.");
                        return;
                    }
                    db.Gearboxes.Add(new Gearbox { Name = name });
                    break;

                case "Локации":
                    if (db.Locations.Any(x => x.Name == name))
                    {
                        MessageBox.Show("Takuv zapis sushtestvuva.");
                        return;
                    }
                    db.Locations.Add(new Location { Name = name });
                    break;
            }

            db.SaveChanges();
            MessageBox.Show("Записът е добавен.");

            comboBox1_SelectedIndexChanged(null, null);
            textBox1.Clear();
        }


        // ======================================================
        // BUTTON 2 – EDIT (ОРИГИНАЛ)
        // ======================================================
        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Избери запис за редакция.");
                return;
            }

            string name = textBox1.Text.Trim();
            if (name == "")
            {
                MessageBox.Show("Името не може да е празно.");
                return;
            }

            switch (comboBox1.SelectedItem.ToString())
            {
                case "Марки":
                    if (db.Brands.Any(x => x.Name == name && x.ID != selectedId))
                    {
                        MessageBox.Show("Такъв запис съществува.");
                        return;
                    }
                    db.Brands.First(x => x.ID == selectedId).Name = name;
                    break;

                case "Категории":
                    if (db.Categories.Any(x => x.Name == name && x.ID != selectedId))
                    {
                        MessageBox.Show("Такъв запис съществува.");
                        return;
                    }
                    db.Categories.First(x => x.ID == selectedId).Name = name;
                    break;

                case "Горива":
                    if (db.Fuels.Any(x => x.Name == name && x.ID != selectedId))
                    {
                        MessageBox.Show("Такъв запис съществува.");
                        return;
                    }
                    db.Fuels.First(x => x.ID == selectedId).Name = name;
                    break;

                case "Скоростни кутии":
                    if (db.Gearboxes.Any(x => x.Name == name && x.ID != selectedId))
                    {
                        MessageBox.Show("Такъв запис съществува.");
                        return;
                    }
                    db.Gearboxes.First(x => x.ID == selectedId).Name = name;
                    break;

                case "Локации":
                    if (db.Locations.Any(x => x.Name == name && x.ID != selectedId))
                    {
                        MessageBox.Show("Такъв запис съществува.");
                        return;
                    }
                    db.Locations.First(x => x.ID == selectedId).Name = name;
                    break;
            }

            db.SaveChanges();
            MessageBox.Show("Записът е редактиран.");

            comboBox1_SelectedIndexChanged(null, null);
            textBox1.Clear();
            selectedId = -1;
        }


        // ======================================================
        // BUTTON 3 – DELETE (ОРИГИНАЛ)
        // ======================================================
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                if (selectedId == -1)
                {
                    MessageBox.Show("Избери запис за изтриване.");
                    return;
                }

                var result = MessageBox.Show(
                    "Сигурен ли си, че искаш да изтриеш?",
                    "Потвърдете",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes) return;

                switch (comboBox1.SelectedItem.ToString())
                {
                    case "Марки":
                        db.Brands.Remove(db.Brands.First(x => x.ID == selectedId));
                        break;

                    case "Категории":
                        db.Categories.Remove(db.Categories.First(x => x.ID == selectedId));
                        break;

                    case "Горива":
                        db.Fuels.Remove(db.Fuels.First(x => x.ID == selectedId));
                        break;

                    case "Скоростни кутии":
                        db.Gearboxes.Remove(db.Gearboxes.First(x => x.ID == selectedId));
                        break;

                    case "Локации":
                        db.Locations.Remove(db.Locations.First(x => x.ID == selectedId));
                        break;
                }

                db.SaveChanges();
                MessageBox.Show("Записът е изтрит.");

                comboBox1_SelectedIndexChanged(null, null);
                textBox1.Clear();
                selectedId = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при изтриване: " + ex.Message);
                db.Dispose();
                db = new RentACarDBEntities(); 
            }
        }


        // ======================================================
        // BUTTON 4 – ADD CAR
        // ======================================================
        private void button4_Click(object sender, EventArgs e)
        {
            // Проверка дали сме в режим "Коли"
            if (comboBox1.Text != "Коли")
            {
                MessageBox.Show("Избери 'Коли' от списъка.");
                return;
            }

            // Проверки за празни полета
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Моля, попълни всички полета.");
                return;
            }

            // Проверка за VIN дължина
            if (textBox2.Text.Length != 17)
            {
                MessageBox.Show("VIN номерът трябва да е точно 17 символа.");
                return;
            }

            // Проверка за уникален VIN
            if (db.Cars.Any(x => x.VIN == textBox2.Text))
            {
                MessageBox.Show("Вече съществува кола с този VIN.");
                return;
            }

            // Проверка за числа
            if (!int.TryParse(textBox4.Text, out int odometer) ||
                !int.TryParse(textBox5.Text, out int year))
            {
                MessageBox.Show("Оdometer и година трябва да са числа.");
                return;
            }

            db.Cars.Add(new Car
            {
                VIN = textBox2.Text.Trim(),
                Model = textBox3.Text.Trim(),
                Odometer = odometer,
                YearOfProduction = year,
                CategoryId = (int)comboBox2.SelectedValue,
                BrandId = (int)comboBox3.SelectedValue,
                LocationId = (int)comboBox4.SelectedValue,
                FuelId = (int)comboBox5.SelectedValue,
                GearboxId = (int)comboBox6.SelectedValue
            });

            db.SaveChanges();
            MessageBox.Show("Колата е добавена успешно.");
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;
            comboBox1_SelectedIndexChanged(null, null);
        }

        // ======================================================
        // BUTTON 5 – EDIT CAR
        // ======================================================
        private void button5_Click(object sender, EventArgs e)
        {
            if (selectedCarId == -1)
            {
                MessageBox.Show("Избери кола за редакция.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Моля, попълни всички полета.");
                return;
            }

            if (!int.TryParse(textBox4.Text, out int odometer) ||
                !int.TryParse(textBox5.Text, out int year))
            {
                MessageBox.Show("Одометър и година трябва да са числа.");
                return;
            }

            var car = db.Cars.Find(selectedCarId);

            // Проверка за VIN конфликт (без себе си)
            if (db.Cars.Any(x => x.VIN == textBox2.Text && x.ID != selectedCarId))
            {
                MessageBox.Show("Вече има друга кола с този VIN.");
                return;
            }

            car.VIN = textBox2.Text.Trim();
            car.Model = textBox3.Text.Trim();
            car.Odometer = odometer;
            car.YearOfProduction = year;
            car.CategoryId = (int)comboBox2.SelectedValue;
            car.BrandId = (int)comboBox3.SelectedValue;
            car.LocationId = (int)comboBox4.SelectedValue;
            car.FuelId = (int)comboBox5.SelectedValue;
            car.GearboxId = (int)comboBox6.SelectedValue;

            db.SaveChanges();
            MessageBox.Show("Колата е редактирана успешно.");
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;

            comboBox1_SelectedIndexChanged(null, null);
        }


        // ======================================================
        // BUTTON 6 – DELETE CAR
        // ======================================================
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedCarId == -1)
                {
                    MessageBox.Show("Избери кола за изтриване.");
                    return;
                }

                if (MessageBox.Show(
                    "Сигурен ли си, че искаш да изтриеш тази кола?",
                    "Потвърдение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                db.Cars.Remove(db.Cars.Find(selectedCarId));
                db.SaveChanges();

                MessageBox.Show("Колата е изтрита успешно.");
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox4.Clear();
                textBox5.Clear();
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                comboBox4.SelectedIndex = -1;
                comboBox5.SelectedIndex = -1;
                comboBox6.SelectedIndex = -1;
                selectedCarId = -1;
                comboBox1_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при изтриване: " + ex.Message);
                db.Dispose();
                db = new RentACarDBEntities();
            }
        }


        // ====== ПРАЗНИ СЪБИТИЯ (ЗА DESIGNER) ======
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e) { }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверява дали символът е цифра или Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Ако не е, блокира въвеждането
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверява дали символът е цифра или главна латинска буква
            if (!char.IsDigit(e.KeyChar) && !(e.KeyChar >= 'A' && e.KeyChar <= 'Z') && e.KeyChar != (char)Keys.Back)
            {
                // Ако не е, блокира въведеното
                e.Handled = true;
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        // ======================================================
        // BUTTON 7 – ADD CUSTOMER
        // ======================================================
        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "Клиенти")
            {
                MessageBox.Show("Избери 'Клиенти' от списъка.");
                return;
            }

            var fullName = textBox6.Text.Trim();
            var phone = textBox7.Text.Trim();
            var license = textBox8.Text.Trim();

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(license) || string.IsNullOrWhiteSpace(license))
            {
                MessageBox.Show("Попълни данни.");
                return;
            }

            // Prevent duplicate license numbers
            if (db.Customers.Any(c => c.LicenseNumber == license))
            {
                MessageBox.Show("Клиент с този номер на лиценза вече съществува.");
                return;
            }

            var customer = new Customer
            {
                FullName = fullName,
                PhoneNumber = phone,
                LicenseNumber = license
            };

            db.Customers.Add(customer);
            db.SaveChanges();

            MessageBox.Show("Клиентът е добавен успешно.");

            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            selectedClientId = -1;

            comboBox1_SelectedIndexChanged(null, null);
        }

        // ======================================================
        // BUTTON 8 – EDIT CUSTOMER
        // ======================================================
        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "Клиенти")
            {
                MessageBox.Show("Избери 'Клиенти' от списъка.");
                return;
            }

            if (selectedClientId == -1)
            {
                MessageBox.Show("Избери клиент за редакция.");
                return;
            }

            var fullName = textBox6.Text.Trim();
            var phone = textBox7.Text.Trim();
            var license = textBox8.Text.Trim();

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(license))
            {
                MessageBox.Show("Попълни име и номер на лиценза.");
                return;
            }

            // Check for license conflict (exclude current)
            if (db.Customers.Any(c => c.LicenseNumber == license && c.ID != selectedClientId))
            {
                MessageBox.Show("Друг клиент вече използва този номер на лиценза.");
                return;
            }

            var customer = db.Customers.Find(selectedClientId);
            if (customer == null)
            {
                MessageBox.Show("Избраният клиент не е намерен.");
                return;
            }

            customer.FullName = fullName;
            customer.PhoneNumber = phone;
            customer.LicenseNumber = license;

            db.SaveChanges();

            MessageBox.Show("Клиентът е редактиран успешно.");

            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            selectedClientId = -1;

            comboBox1_SelectedIndexChanged(null, null);
        }

        // ======================================================
        // BUTTON 9 – DELETE CUSTOMER
        // ======================================================
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text != "Клиенти")
                {
                    MessageBox.Show("Избери 'Клиенти' от списъка.");
                    return;
                }

                if (selectedClientId == -1)
                {
                    MessageBox.Show("Избери клиент за изтриване.");
                    return;
                }

                if (MessageBox.Show(
                    "Сигурен ли си, че искаш да изтриеш този клиент?",
                    "Потвърждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }

                var customer = db.Customers.Find(selectedClientId);
                if (customer == null)
                {
                    MessageBox.Show("Избраният клиент не е намерен.");
                    return;
                }

                db.Customers.Remove(customer);
                db.SaveChanges();

                MessageBox.Show("Клиентът е изтрит успешно.");

                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                selectedClientId = -1;

                comboBox1_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при изтриване: " + ex.Message);
                db.Dispose();
                db = new RentACarDBEntities();
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            // Add reservation (validate before saving)
            if (comboBox1.Text != "Резервации")
            {
                MessageBox.Show("Избери 'Резервации' от списъка.");
                return;
            }

            if (comboBox7.SelectedValue == null)
            {
                MessageBox.Show("Моля, изберете клиент.");
                return;
            }

            if (comboBox8.SelectedValue == null)
            {
                MessageBox.Show("Моля, изберете кола.");
                return;
            }

            if (comboBox9.SelectedValue == null)
            {
                MessageBox.Show("Моля, изберете локация.");
                return;
            }

            // Require explicit dates (designer may enable Checked)
            if (!dateTimePicker1.Checked || !dateTimePicker2.Checked)
            {
                MessageBox.Show("Моля, изберете дата.");
                return;
            }

            DateTime from = dateTimePicker1.Value.Date;
            DateTime to = dateTimePicker2.Value.Date;

            // Return date must not be before rent date
            if (to < from)
            {
                MessageBox.Show("Крайната дата трябва да бъде след началната.");
                return;
            }

            int customerId;
            int carId;
            int locationId;
            try
            {
                customerId = Convert.ToInt32(comboBox7.SelectedValue);
                carId = Convert.ToInt32(comboBox8.SelectedValue);
                locationId = Convert.ToInt32(comboBox9.SelectedValue);
            }
            catch
            {
                MessageBox.Show("Невалидни избори за клиент/кола/локация.");
                return;
            }

            // Verify car exists and is from selected location
            var car = db.Cars.Find(carId);
            if (car == null)
            {
                MessageBox.Show("Избраната кола не е намерена.");
                return;
            }
            if (car.LocationId != locationId)
            {
                MessageBox.Show("Тази кола не е в избрания град.");
                return;
            }

            // Check if customer already has a reservation that overlaps the requested interval
            bool customerHasOverlap = db.Reservations.Any(r =>
                r.CustomerId == customerId &&
                !(r.ToDate < from || r.FromDate > to)); // overlap if NOT (r.To < from OR r.From > to)

            if (customerHasOverlap)
            {
                MessageBox.Show("Клиентът вече има резервация, която (частично или напълно) се припокрива с този период.");
                return;
            }

            // Check if the car is free for these dates
            bool carIsTaken = db.Reservations.Any(r =>
                r.CarId == carId &&
                !(r.ToDate < from || r.FromDate > to));

            if (carIsTaken)
            {
                MessageBox.Show("Колата не е свободна за избрания период.");
                return;
            }

            // Validate amount format: allow both comma and dot as decimal separators, require exactly two decimals
            var amountTextRaw = textBox11.Text?.Trim();
            if (string.IsNullOrEmpty(amountTextRaw))
            {
                MessageBox.Show("Въведете сума.");
                return;
            }

            // Normalize comma to dot so regex / parse are consistent
            var amountText = amountTextRaw.Replace(',', '.');

            // Require exactly two decimal places
            if (!System.Text.RegularExpressions.Regex.IsMatch(amountText, @"^\d+(\.\d{2})$"))
            {
                MessageBox.Show("Сумата трябва да е във формат с две цифри след точката, например 123.45");
                return;
            }

            if (!decimal.TryParse(amountText, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out decimal amount))
            {
                MessageBox.Show("Невалиден формат на сумата.");
                return;
            }

            // Normalize/store the amount rounded up to 2 decimals (keeps consistency with grid display)
            decimal storedAmount = RoundUpToTwoDecimals(amount);

            // All validations passed — create reservation entity
            var reservation = new Reservation
            {
                CustomerId = customerId,
                CarId = carId,
                FromDate = dateTimePicker1.Value,
                ToDate = dateTimePicker2.Value,
                Amount = storedAmount
            };

            // Save in try/catch to surface any exception details (DB schema, constraints, etc.)
            try
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                // Show detailed message so you can paste the exception if it still fails
                var inner = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show("Грешка при запис: " + inner, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Резервацията е добавена успешно.");

            // Refresh only the grid (do not clear panel fields)
            dataGridView1.DataSource = db.Reservations
                .AsEnumerable()
                .Select(r => new
                {
                    r.CustomerId,
                    Customer = r.Customer.FullName,
                    r.CarId,
                    Car = r.Car.Model,
                    Location = r.Car.Location.Name,
                    r.FromDate,
                    r.ToDate,
                    Amount = RoundUpToTwoDecimals(r.Amount)
                })
                .ToList();
            ClearReservationPanel();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            // EDIT reservation (primary key = CustomerId, CarId, FromDate)
            if (comboBox1.Text != "Резервации")
            {
                MessageBox.Show("Избери 'Резервации' от списъка.");
                return;
            }

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Избери резервация за редакция.");
                return;
            }

            // Read selected reservation composite key from grid row
            var row = dataGridView1.SelectedRows[0];
            var custObj = GetCellValueFromRow(row, "CustomerId", "Customer");
            var carObj = GetCellValueFromRow(row, "CarId", "Car");
            var fromObj = GetCellValueFromRow(row, "FromDate", "From");

            if (custObj == null || carObj == null || fromObj == null)
            {
                MessageBox.Show("Не може да прочете полетата на избраната резервация.");
                return;
            }

            int origCustomerId;
            int origCarId;
            DateTime origFromDate;
            try
            {
                origCustomerId = Convert.ToInt32(custObj);
                origCarId = Convert.ToInt32(carObj);
                origFromDate = Convert.ToDateTime(fromObj).Date;
            }
            catch
            {
                MessageBox.Show("Невалидни данни в избраната резервация.");
                return;
            }

            // Load existing reservation entity
            var existing = db.Reservations
                .Where(r => r.CustomerId == origCustomerId && r.CarId == origCarId)
                .AsEnumerable()
                .FirstOrDefault(r => r.FromDate.Date == origFromDate);

            if (existing == null)
            {
                MessageBox.Show("Резервацията не е намерена (възможно е вече да е изтрита).");
                comboBox1_SelectedIndexChanged(null, null);
                return;
            }

            // Gather new values from panel
            if (comboBox7.SelectedValue == null)
            {
                MessageBox.Show("Моля, изберете клиент.");
                return;
            }
            if (comboBox8.SelectedValue == null)
            {
                MessageBox.Show("Моля, изберете кола.");
                return;
            }
            if (comboBox9.SelectedValue == null)
            {
                MessageBox.Show("Моля, изберете локация.");
                return;
            }
            if (!dateTimePicker1.Checked || !dateTimePicker2.Checked)
            {
                MessageBox.Show("Моля, изберете дати.");
                // keep other fields intact, only clear the problematic date pickers so user must re-enter
                try { dateTimePicker1.Checked = false; } catch { }
                try { dateTimePicker2.Checked = false; } catch { }
                return;
            }

            int newCustomerId;
            int newCarId;
            int newLocationId;
            try
            {
                newCustomerId = Convert.ToInt32(comboBox7.SelectedValue);
                newCarId = Convert.ToInt32(comboBox8.SelectedValue);
                newLocationId = Convert.ToInt32(comboBox9.SelectedValue);
            }
            catch
            {
                MessageBox.Show("Невалидни избори за клиент/кола/локация.");
                return;
            }

            DateTime newFrom = dateTimePicker1.Value.Date;
            DateTime newTo = dateTimePicker2.Value.Date;

            // Validate date order
            if (newTo < newFrom)
            {
                MessageBox.Show("Крайната дата трябва да бъде след началната.");
                // clear the problematic field (return date)
                try
                {
                    suppressDateValidation = true;
                    dateTimePicker2.Checked = false;
                }
                finally { suppressDateValidation = false; }
                return;
            }

            // Verify car exists and belongs to selected location
            var car = db.Cars.Find(newCarId);
            if (car == null)
            {
                MessageBox.Show("Избраната кола не е намерена.");
                comboBox8.SelectedIndex = -1;
                return;
            }
            if (car.LocationId != newLocationId)
            {
                MessageBox.Show("Избраната кола не е в избраната локация.");
                // clear problematic field (car)
                comboBox8.SelectedIndex = -1;
                return;
            }

            // Validate amount format
            var amountTextRaw = textBox11.Text?.Trim();
            if (string.IsNullOrEmpty(amountTextRaw))
            {
                MessageBox.Show("Въведете сума.");
                textBox11.Clear();
                return;
            }
            var amountText = amountTextRaw.Replace(',', '.');
            if (!System.Text.RegularExpressions.Regex.IsMatch(amountText, @"^\d+(\.\d{2})$"))
            {
                MessageBox.Show("Сумата трябва да е във формат с две цифри след запетаята, например 123.45");
                textBox11.Clear();
                return;
            }
            if (!decimal.TryParse(amountText, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out decimal parsedAmount))
            {
                MessageBox.Show("Невалиден формат на сумата.");
                textBox11.Clear();
                return;
            }
            decimal storedAmount = RoundUpToTwoDecimals(parsedAmount);

            // --- IMPORTANT FIX: Avoid DbFunctions.Date translation errors by fetching only relevant reservations into memory ---
            // Check customer overlap (exclude the original reservation when checking)
            var customerReservations = db.Reservations
                .Where(r => r.CustomerId == newCustomerId)
                .AsEnumerable(); // switch to LINQ-to-Objects for safe Date comparisons

            bool customerHasOverlap = customerReservations.Any(r =>
                !(r.ToDate.Date < newFrom || r.FromDate.Date > newTo) &&
                !(r.CustomerId == origCustomerId && r.CarId == origCarId && r.FromDate.Date == origFromDate));

            if (customerHasOverlap)
            {
                MessageBox.Show("Клиентът вече има резервация, която се припокрива с този период.");
                // keep other fields, clear only client selection to prompt user
                comboBox7.SelectedIndex = -1;
                return;
            }

            // Check car availability for new interval (exclude the original reservation)
            var carReservations = db.Reservations
                .Where(r => r.CarId == newCarId)
                .AsEnumerable();

            bool carIsTaken = carReservations.Any(r =>
                !(r.ToDate.Date < newFrom || r.FromDate.Date > newTo) &&
                !(r.CustomerId == origCustomerId && r.CarId == origCarId && r.FromDate.Date == origFromDate));

            if (carIsTaken)
            {
                MessageBox.Show("Колата не е свободна за избрания период.");
                comboBox8.SelectedIndex = -1;
                return;
            }

            // All validations passed — perform update.
            try
            {
                // If primary key unchanged, update existing
                if (origCustomerId == newCustomerId && origCarId == newCarId && origFromDate == newFrom)
                {
                    existing.ToDate = dateTimePicker2.Value;
                    existing.Amount = storedAmount;
                    db.SaveChanges();
                }
                else
                {
                    // Primary key changed: create new reservation and remove the old one
                    var newReservation = new Reservation
                    {
                        CustomerId = newCustomerId,
                        CarId = newCarId,
                        FromDate = dateTimePicker1.Value,
                        ToDate = dateTimePicker2.Value,
                        Amount = storedAmount
                    };

                    db.Reservations.Add(newReservation);
                    db.Reservations.Remove(existing);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show("Грешка при запис: " + inner, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Резервацията е редактирана успешно.");

            // Refresh reservations grid (include Location)
            dataGridView1.DataSource = db.Reservations
                .AsEnumerable()
                .Select(r => new
                {
                    r.CustomerId,
                    Customer = r.Customer.FullName,
                    r.CarId,
                    Car = r.Car.Model,
                    Location = r.Car.Location.Name,
                    r.FromDate,
                    r.ToDate,
                    Amount = RoundUpToTwoDecimals(r.Amount)
                })
                .ToList();

            // Clear reservation inputs
            ClearReservationPanel();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox7.SelectedIndex == -1 || comboBox8.SelectedIndex == -1)
                {
                    MessageBox.Show("Изберете резервация за изтриване");
                    ClearReservationPanel();
                    return;
                }

                int customerId = (int)comboBox7.SelectedValue;
                int carId = (int)comboBox8.SelectedValue;
                DateTime fromDate = dateTimePicker1.Value.Date;
                DateTime toDate = dateTimePicker2.Value.Date;

                decimal panelAmount;
                bool amountParsed = decimal.TryParse(textBox11.Text, out panelAmount);

                if (!amountParsed)
                {
                    MessageBox.Show("Изберете резервация за изтриване");
                    ClearReservationPanel();
                    return;
                }
                DateTime nextday = fromDate.AddDays(1);
                var reservation = db.Reservations.FirstOrDefault(r =>
                    r.CustomerId == customerId &&
                    r.CarId == carId && r.FromDate >= fromDate && r.FromDate < nextday);

                if (reservation == null)
                {
                    MessageBox.Show("Изберете резервация за изтриване");
                    ClearReservationPanel();
                    return;
                }

                decimal dbAmount = Math.Round(reservation.Amount, 2);
                decimal enteredAmount = Math.Round(panelAmount, 2);

                if (reservation.ToDate.Date != toDate || reservation.FromDate.Date != fromDate || enteredAmount != dbAmount)
                {
                    MessageBox.Show("Изберете резервация за изтриване");
                    ClearReservationPanel();
                    return;
                }

                var result = MessageBox.Show(
                    "Сигурни ли сте, че искате да изтриете резервацията?",
                    "Потвърждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                    return;

                db.Reservations.Remove(reservation);
                db.SaveChanges();

                MessageBox.Show("Резервацията е изтрита успешно");
                dataGridView1.DataSource = db.Reservations
                    .AsEnumerable()
                    .Select(r => new
                    {
                        r.CustomerId,
                        Customer = r.Customer.FullName,
                        r.CarId,
                        Car = r.Car.Model,
                        Location = r.Car.Location.Name,
                        r.FromDate,
                        r.ToDate,
                        Amount = RoundUpToTwoDecimals(r.Amount)
                    })
                    .ToList();

                ClearReservationPanel();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при изтриване: " + ex.Message);
                db.Dispose();
                db = new RentACarDBEntities();
            }
        }

        // Clear inputs in the reservations panel (panel 4)
        private void ClearReservationPanel()
        {
            try { comboBox7.SelectedIndex = -1; } catch { }
            try
            {
                comboBox8.DataSource = null;
                comboBox8.Items.Clear();
                comboBox8.SelectedIndex = -1;
            }
            catch { }
            try { comboBox9.SelectedIndex = -1; } catch { }

            // suppress date validation while resetting
            suppressDateValidation = true;
            try
            {
                dateTimePicker1.Value = DateTime.Today;
                dateTimePicker2.Value = DateTime.Today;
            }
            finally
            {
                suppressDateValidation = false;
            }

            // update last valid dates after programmatic reset
            lastValidReturnDate = dateTimePicker2.Value;
            lastValidRentDate = dateTimePicker1.Value;

            try { textBox11.Clear(); } catch { }
            selectedReservationId = -1;
        }

        // ======================================================
        // comboBox9 (Location) changed — populate comboBox8 with cars only for this location
        // ======================================================
        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If comboBox9 has no selection, clear comboBox8
            if (comboBox9.SelectedValue == null || comboBox9.SelectedIndex == -1)
            {
                comboBox8.DataSource = null;
                comboBox8.Items.Clear();
                comboBox8.SelectedIndex = -1;
                return;
            }

            // Do not require dates to populate the car list. If both date pickers are checked
            // validate the range; otherwise ignore dates and show all cars for the location.
            if (!suppressDateValidation && dateTimePicker1.Checked && dateTimePicker2.Checked)
            {
                DateTime from = dateTimePicker1.Value.Date;
                DateTime to = dateTimePicker2.Value.Date;

                // Validate date range only when both pickers are explicitly checked
                if (to < from)
                {
                    MessageBox.Show("Крайната дата трябва да бъде след началната.");
                    return;
                }
            }

            int locationId;
            try
            {
                locationId = Convert.ToInt32(comboBox9.SelectedValue);
            }
            catch
            {
                comboBox8.DataSource = null;
                comboBox8.Items.Clear();
                comboBox8.SelectedIndex = -1;
                return;
            }

            // Load ALL cars that belong to the selected location (do not filter by reservations/dates).
            var cars = db.Cars
                .Where(c => c.LocationId == locationId)
                .OrderBy(c => c.Model)
                .Select(c => new { c.ID, Display = c.Model })
                .ToList();

            if (cars.Count == 0)
            {
                comboBox8.DataSource = null;
                comboBox8.Items.Clear();
                comboBox8.SelectedIndex = -1;
                MessageBox.Show("Няма коли в този град.");
                return;
            }

            comboBox8.DataSource = cars;
            comboBox8.DisplayMember = "Display";
            comboBox8.ValueMember = "ID";
            comboBox8.SelectedIndex = -1;
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        // store current value when user begins editing the return date
        private void dateTimePicker2_Enter(object sender, EventArgs e)
        {
            if (suppressDateValidation) return;
            lastValidReturnDate = dateTimePicker2.Value;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (suppressDateValidation)
            {
                // when suppressed, avoid validation/messages and do not alter lastValid here
                return;
            }

            // If return date becomes earlier than rent date, show message and revert to last valid value
            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                MessageBox.Show("Датата за връщане трябва да бъде след датата на наемане на автомобила.", "Грешка");

                // Temporarily unsubscribe to avoid recursive ValueChanged calls
                dateTimePicker2.ValueChanged -= dateTimePicker2_ValueChanged;
                try
                {
                    dateTimePicker2.Value = lastValidReturnDate;
                }
                finally
                {
                    // Re-subscribe
                    dateTimePicker2.ValueChanged += dateTimePicker2_ValueChanged;
                }
                return;
            }

            // update last valid when the new value is acceptable
            lastValidReturnDate = dateTimePicker2.Value;
        }

        // store current value when user begins editing the rent date
        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
            if (suppressDateValidation) return;
            lastValidRentDate = dateTimePicker1.Value;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (suppressDateValidation)
            {
                // suppressed during programmatic population, ignore
                return;
            }

            // If rent date becomes later than return date, show message and revert to last valid value
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("Датата на наемане трябва да бъде преди датата за връщане.", "Грешка");

                // Temporarily unsubscribe to avoid recursive ValueChanged calls
                dateTimePicker1.ValueChanged -= dateTimePicker1_ValueChanged;
                try
                {
                    dateTimePicker1.Value = lastValidRentDate;
                }
                finally
                {
                    // Re-subscribe
                    dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
                }
                return;
            }

            // update last valid when the new value is acceptable
            lastValidRentDate = dateTimePicker1.Value;
        }
    }
}
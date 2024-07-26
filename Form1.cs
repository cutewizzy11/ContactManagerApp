using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ContactManagerApp
{
    public partial class Form1 : Form
    {
        private const string FilePath = "contacts.txt";
        private List<Contact> contacts = new List<Contact>();

        public Form1()
        {
            InitializeComponent();
            LoadContacts();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string firstName = textBoxFirstName.Text.Trim();
            string lastName = textBoxLastName.Text.Trim();
            string phoneNumber = textBoxPhoneNumber.Text.Trim();
            string email = textBoxEmail.Text.Trim();

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                Contact newContact = new Contact(firstName, lastName, phoneNumber, email);
                contacts.Add(newContact);
                listBoxContacts.Items.Add(newContact);
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("First Name and Last Name are required.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listBoxContacts.SelectedIndex >= 0)
            {
                Contact selectedContact = (Contact)listBoxContacts.SelectedItem;
                selectedContact.FirstName = textBoxFirstName.Text.Trim();
                selectedContact.LastName = textBoxLastName.Text.Trim();
                selectedContact.PhoneNumber = textBoxPhoneNumber.Text.Trim();
                selectedContact.Email = textBoxEmail.Text.Trim();

                listBoxContacts.Items[listBoxContacts.SelectedIndex] = selectedContact;
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Please select a contact to edit.", "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxContacts.SelectedIndex >= 0)
            {
                contacts.RemoveAt(listBoxContacts.SelectedIndex);
                listBoxContacts.Items.RemoveAt(listBoxContacts.SelectedIndex);
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Please select a contact to delete.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveContacts();
            MessageBox.Show("Contacts saved successfully.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadContacts()
        {
            if (File.Exists(FilePath))
            {
                string[] lines = File.ReadAllLines(FilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 4)
                    {
                        Contact contact = new Contact(parts[0], parts[1], parts[2], parts[3]);
                        contacts.Add(contact);
                        listBoxContacts.Items.Add(contact);
                    }
                }
            }
        }

        private void SaveContacts()
        {
            List<string> lines = contacts.Select(c => $"{c.FirstName}|{c.LastName}|{c.PhoneNumber}|{c.Email}").ToList();
            File.WriteAllLines(FilePath, lines);
        }

        private void ClearInputFields()
        {
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxPhoneNumber.Clear();
            textBoxEmail.Clear();
        }
    }

    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Contact(string firstName, string lastName, string phoneNumber, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} - {PhoneNumber} - {Email}";
        }
    }
}

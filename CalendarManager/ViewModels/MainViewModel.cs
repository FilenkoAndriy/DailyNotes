﻿using CalendarManager.Core;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CalendarManager.Views;

namespace CalendarManager.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private string filepath = "notes.json";
        private DateTime selectedDate;
        private Note selectedNote;
        private ObservableCollection<Note> notes = new ObservableCollection<Note>();
        private RelayCommand addCommand;
        private RelayCommand removeCommand;

        public ObservableCollection<Note> Notes
        {
            get { return notes; }
            set
            {
                notes = value;
                OnPropertyChanged("Notes");
            }
        }

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                Notes = GetNotes(selectedDate);
                OnPropertyChanged("SelectedDate");
            }
        }

        public Note SelectedNote
        {
            get { return selectedNote; }
            set
            {
                selectedNote = value;
                OnPropertyChanged("SelectedNote");
            }
        }

        public ObservableCollection<Note> GetNotes(DateTime date)
        {
            List<Note> items = ReadJson();
            ObservableCollection<Note> notes = new ObservableCollection<Note>(items.Where(i => i.Date == date));
            
            return notes;
        }
        
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      AddNoteWindow addNoteWindow = new AddNoteWindow();
                      AddNoteViewModel addNoteViewModel = new AddNoteViewModel(selectedDate);
                      addNoteWindow.DataContext = addNoteViewModel;
                      addNoteWindow.ShowDialog();
                      AddNote(addNoteViewModel.Note);
                  }));
            }
        }

        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {
                        RemoveNote(selectedNote);
                    }
                    ));
            }
        }
        
        public void AddNote(Note note)
        {
            Notes.Add(note);
            List<Note> allNotes = ReadJson();
            allNotes.Add(note);
            WriteJson(allNotes);
        }

        public void RemoveNote(Note note)
        {
            List<Note> allNotes = ReadJson();
            Note noteToDelete = null;

            foreach (Note n in allNotes)
            {
                if (n.Id == note.Id)
                {
                    noteToDelete = n;
                }
            }

            if(noteToDelete != null)
            {
                allNotes.Remove(noteToDelete);
                notes.Remove(note);
            }

            WriteJson(allNotes);
        }

        public List<Note> ReadJson()
        {
            List<Note> items = new List<Note>();
            using (StreamReader r = new StreamReader(filepath))
            {
                string jsonRead = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Note>>(jsonRead);
            }

            return items;
        }
        public void WriteJson(List<Note> notesToFile)
        {
            string jsonWrite = JsonConvert.SerializeObject(notesToFile);
            File.WriteAllText(filepath, jsonWrite);
        }

    }
}

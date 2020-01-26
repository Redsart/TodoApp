﻿using System;
using System.Collections.Generic;
using TodoApp.Library.Models;
using System.Xml;
using System.Globalization;

namespace TodoApp.Library.Data
{
    public class XMLTaskReader
    {
        const string path = "../../tasks.xml";
        public static TimeSpan TimeLeft(DateTime startDate,DateTime endDate)
        {
            TimeSpan timeLeft = endDate.Subtract(startDate).Duration();

            return timeLeft;
        }

        public List<Task> ReadTasks()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes;
            nodes = doc.SelectNodes("tasks/task");
            List<Task> tasks = new List<Task>();

            foreach (XmlNode node in nodes)
            {
                string title = node.SelectSingleNode("title").InnerText;
                string message = node.SelectSingleNode("description").InnerText;
                string start = node.SelectSingleNode("dateCreated").InnerText;
                DateTime startDate = DateTime.ParseExact(start, "dd MM yyyy", CultureInfo.InvariantCulture);
                string end = node.SelectSingleNode("deadline").InnerText;
                DateTime endDate = DateTime.ParseExact(end, "dd MM yyyy", CultureInfo.InvariantCulture);
                int deadline = TimeLeft(startDate, endDate).Days;
                string guid = node.Attributes["id"].Value;

                Task task = new Task(title, message, deadline)
                {
                    ID = Guid.Parse(guid)
                };
                tasks.Add(task);
            }

            return tasks;
        }
    }
}

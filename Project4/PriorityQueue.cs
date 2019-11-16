﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Registration Simulation
//	File Name:		PriorityQueue.cs
//	Description:	Customized queue that accounts for the priority of the objects contained 
//	Course:			CSCI 2210-001 - Data Structures
//	Author:			Michael Edwards, edwardsmr@etsu.edu, Elizabeth Jennings, jenningsel@etsu.edu, William Jennings, jenningsw@etsu.edu
//	Created:		Sunday November 14, 2019
//	Copyright:		Michael Edwards, 2019
//
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4 {
	/// <summary>
	/// Interface to be implemented that holds functions to determine typical properties of a container
	/// </summary>
	/// <typeparam name="T">Generic object, must implement IComparable</typeparam>
	public interface IContainer<T> {
		void Clear();
		bool IsEmpty();
		int Count { get; set; }
	}
	/// <summary>
	/// Interface to be implemented that holds functions that mimic typical functionality of a Queue, though customized for considering priority.
	/// </summary>
	/// <typeparam name="T">Generic Object, must implement IComparable</typeparam>
	public interface IPriorityQueue<T>:IContainer<T> where T : IComparable {
		void Enqueue(T item);
		T Dequeue();
		T Peek();
	}
	/// <summary>
	/// Node class for a priority queue, holds the data type of the queue
	/// </summary>
	/// <typeparam name="T">Generic object, must implement IComparable</typeparam>
	public class Node<T> where T : IComparable {
		public T Item { get; set; }
		public Node<T> Next { get; set; }
		/// <summary>
		/// Node overloaded constructor - instantiates a node with the object passed in
		/// </summary>
		/// <param name="value">Generic object, must implement IComparable</param>
		/// <param name="link">The node behind the current in the Queue, if not null</param>
		public Node(T value, Node<T> link) {
			Item = value;
			Next = link;
		}
	}
	/// <summary>
	/// Priority Queue class, contains generic object T
	/// </summary>
	/// <typeparam name="T">Generic object, must implement IComparable</typeparam>
	public class PriorityQueue<T>:IPriorityQueue<T> where T : IComparable {
		/// <summary>
		/// The top node in the queue
		/// </summary>
		private Node<T> top;
		/// <summary>
		/// The total nodes in the queue
		/// </summary>
		public int Count { get; set; }
		/// <summary>
		/// Clears the queue
		/// </summary>
		public void Clear() {
			top = null;
			Count = 0;
		}
		/// <summary>
		/// Determines if the queue is empty
		/// </summary>
		/// <returns>bool - true if empty, false otherwise</returns>
		public bool IsEmpty() {
			return Count == 0;
		}
		/// <summary>
		/// Adds, or "Enqueues", a new node into the back of the queue
		/// </summary>
		/// <param name="item">The item to be enqueued</param>
		public void Enqueue(T item) {
			if(Count == 0) {
				top = new Node<T>(item, null);
			} else {
				Node<T> current = top;
				Node<T> previous = null;
				while(current != null && current.Item.CompareTo(item) >= 0) {
					previous = current;
					current = current.Next;
				}
				Node<T> newNode = new Node<T>(item, current);
				if(previous != null) {
					previous.Next = newNode;
				} else {
					top = newNode;
				}
			}
			Count++;
		}
		/// <summary>
		/// Removes the top of the queue
		/// </summary>
		public T Dequeue() {
			if(IsEmpty()) {
				throw new InvalidOperationException("Cannot remove from empty Queue.");
			} else {
				Node<T> oldNode = top;
				top = top.Next;
				Count--;
				T item = oldNode.Item;
				oldNode = null; // Garbage collected
				return item;
			}
		}
		/// <summary>
		/// Retrieves the top of the queue without removing it
		/// </summary>
		/// <returns>The object at the top of the queue</returns>
		public T Peek() {
			if(!IsEmpty()) {
				return top.Item;
			} else {
				throw new InvalidOperationException("Cannot obtains top of empty priority queue");
			}
		}
	}
}

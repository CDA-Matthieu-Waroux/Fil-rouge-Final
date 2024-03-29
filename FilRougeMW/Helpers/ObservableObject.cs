﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRougeMW
{
    class ObservableObject : INotifyPropertyChanged
    {   
        #region INotifyPropertyChanged Members
        
        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        
        /// <param name="propertyName">The property that has a new value.</param>
        
        
        public virtual void RaisePropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);
            if (PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, e);
            }
        }
        #endregion // INotifyPropertyChanged Members

        #region Debugging Aides
        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This
        /// method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public virtual void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;
                if (ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }
        /// <summary>
        /// Returns whether an exception is thrown, or if a Debug.Fail() is used
        /// when an invalid property name is passed to the VerifyPropertyName method.
        /// The default value is false, but subclasses used by unit tests might
        /// override this property's getter to return true.
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }
        internal void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion // Debugging Aides
    }

}


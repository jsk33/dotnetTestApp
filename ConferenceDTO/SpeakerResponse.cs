using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceDTO
{
    public class SpeakerResponse : Speaker
    {
        // TODO: Set order of JSON properties so this shows up last not first
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}

// it's better practice to return an output model such as this instead of directly returning a model class 
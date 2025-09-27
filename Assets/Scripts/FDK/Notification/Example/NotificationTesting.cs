using FDK.Notification;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace FDK.Currency
{
    public class NotificationTesting : MonoBehaviour
    {
        [SerializeField]
        private Button _randomNotification;

        private List<string> _elfNames = new List<string>
        {
            "Aelindra Sylvaranth",
            "Liriel Moonshadow",
            "Seraphina Dawnwhisper",
            "Isilme Starborn",
            "Evelynna Frostbloom",
            "Thalindra Riversong",
            "Yvraine Nightbreeze",
            "Sylmara Goldleaf",
            "Amaranthae Dreamweaver",
            "Cerridwen Silverglen",
            "Elowen Frostfern",
            "Nimue Brightvale",
            "Vaelara Emberwind",
            "Faelara Sunspark",
            "Lyria Moonpetal",
            "Aerindel Swiftbrook",
            "Calanthe Duskshade",
            "Elandra Whisperwillow",
            "Melarue Dawnbloom",
            "Sariel Starfrost",
            "Talandra Emberglow",
            "Vespera Shadowthorn",
            "Zylphia Mistvale",
            "Illithia Rosefern",
            "Oleandra Frostgale"
        };

        private IGlobalNotificationService _notificationService;

        [Inject]
        public void Inject(IGlobalNotificationService notificationService)
        {
            _notificationService = notificationService;
            _randomNotification.onClick.AddListener(PickOneNameInRandom);
        }

        private void PickOneNameInRandom()
        {
            var randomIdx = Random.Range(0, _elfNames.Count - 1);
            var name = _elfNames[randomIdx];
            var text = $"{name} Joined Party.";
            _notificationService.ShowNotification(text);
        }

    }
}

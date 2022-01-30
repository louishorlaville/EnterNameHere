/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID AMBIANCE_LEVEL_DARK = 665468665U;
        static const AkUniqueID CUBE_MAGNET_TO_SURFACE = 391978585U;
        static const AkUniqueID CUBE_MOVEMENT = 2048743322U;
        static const AkUniqueID CUBE_STUCK_TO_MAGNET = 591287882U;
        static const AkUniqueID CUBE_TO_SLIME = 3425940391U;
        static const AkUniqueID DEATH_PLAYER_LASER = 1090492715U;
        static const AkUniqueID DEATH_SPIKE = 614286398U;
        static const AkUniqueID END_CREDIT = 718146176U;
        static const AkUniqueID END_FALL = 1232782884U;
        static const AkUniqueID END_PRINCESS = 1668640766U;
        static const AkUniqueID END_SQUISH = 2166129864U;
        static const AkUniqueID INTERUPTEUR = 2327439314U;
        static const AkUniqueID INTRO_GAME = 2261613202U;
        static const AkUniqueID ITEM_DESTRUCTION = 3307081041U;
        static const AkUniqueID LASER_BEAM = 733612088U;
        static const AkUniqueID MAGNET_NEGATIVE = 2874863639U;
        static const AkUniqueID MAINMENUSTOP = 2223599963U;
        static const AkUniqueID MAP_ENDING = 3793843481U;
        static const AkUniqueID MUSHROOM_SOUND = 3973633563U;
        static const AkUniqueID MUSIC_LVL2 = 3380345081U;
        static const AkUniqueID MUSIC_LVL3 = 3380345080U;
        static const AkUniqueID MUSIC_MAINMENU = 599938019U;
        static const AkUniqueID MUSIQUE_LVL_ONE = 1191994856U;
        static const AkUniqueID SLIME_JUMP = 2006797914U;
        static const AkUniqueID SLIME_MOUVEMENT = 1418889888U;
        static const AkUniqueID SLIME_TO_CUBE = 3678733195U;
        static const AkUniqueID UI_PAUSE = 2792155208U;
        static const AkUniqueID VICTORY_MUSIC = 1984291205U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace MAGNET_POLARITY
        {
            static const AkUniqueID GROUP = 4033083518U;

            namespace STATE
            {
                static const AkUniqueID NEGATIVE = 4219547688U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID POSITIVE = 1192865152U;
            } // namespace STATE
        } // namespace MAGNET_POLARITY

        namespace SLIME_FORM
        {
            static const AkUniqueID GROUP = 1999760368U;

            namespace STATE
            {
                static const AkUniqueID CUBE = 4052031814U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID SLIME = 3803999823U;
            } // namespace STATE
        } // namespace SLIME_FORM

    } // namespace STATES

    namespace SWITCHES
    {
        namespace SLIME_FORM
        {
            static const AkUniqueID GROUP = 1999760368U;

            namespace SWITCH
            {
                static const AkUniqueID CUBE = 4052031814U;
                static const AkUniqueID SLIME = 3803999823U;
            } // namespace SWITCH
        } // namespace SLIME_FORM

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID PLAYBACK_RATE = 1524500807U;
        static const AkUniqueID RPM = 796049864U;
        static const AkUniqueID SLIMESPEED = 1467731768U;
        static const AkUniqueID SS_AIR_FEAR = 1351367891U;
        static const AkUniqueID SS_AIR_FREEFALL = 3002758120U;
        static const AkUniqueID SS_AIR_FURY = 1029930033U;
        static const AkUniqueID SS_AIR_MONTH = 2648548617U;
        static const AkUniqueID SS_AIR_PRESENCE = 3847924954U;
        static const AkUniqueID SS_AIR_RPM = 822163944U;
        static const AkUniqueID SS_AIR_SIZE = 3074696722U;
        static const AkUniqueID SS_AIR_STORM = 3715662592U;
        static const AkUniqueID SS_AIR_TIMEOFDAY = 3203397129U;
        static const AkUniqueID SS_AIR_TURBULENCE = 4160247818U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID BANQUELVLTERRE = 2436553819U;
        static const AkUniqueID INTRO = 1125500713U;
        static const AkUniqueID MAINMENU = 3604647259U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MOTION_FACTORY_BUS = 985987111U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__

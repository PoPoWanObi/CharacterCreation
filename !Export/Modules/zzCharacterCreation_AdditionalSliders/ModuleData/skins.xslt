<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output omit-xml-declaration="yes" />
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="man"]/deform_keys/deform_key[@id="weight"]'>
        <deform_key id="TorsoBelly" key_time_point="1001" name="Belly Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_1" axis="x" key_min="0.95" key_max="1.05" />
                <bone_scale bone_type="biped_spine_1" axis="y" key_min="0.95" key_max="1.05" />
                <bone_scale bone_type="biped_spine_1" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoHips" key_time_point="1002" name="Hip Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="x" key_min="0.98" key_max="1.12" />
                <bone_scale bone_type="biped_abdomen" axis="z" key_min="0.97" key_max="1.05" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoButt" key_time_point="1003" name="Butt Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="y" key_min="0.94" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="Thigh" key_time_point="1004" name="Thigh Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thigh_l" axis="x" key_min="0.95" key_max="1.05" />
                <bone_scale bone_type="biped_thigh_l" axis="y" key_min="0.95" key_max="1.05" />
                <bone_scale bone_type="biped_thigh_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_thigh_r" axis="x" key_min="0.95" key_max="1.05" />
                <bone_scale bone_type="biped_thigh_r" axis="y" key_min="0.95" key_max="1.05" />
                <bone_scale bone_type="biped_thigh_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Calf" key_time_point="1005" name="Calf Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_calf_l" axis="x" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="y" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_calf_r" axis="x" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="y" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Foot" key_time_point="1006" name="Foot Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_foot_l" axis="x" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="y" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_foot_r" axis="x" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="y" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoX" key_time_point="1007" name="Torso Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="x" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoY" key_time_point="1008" name="Torso Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="y" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoZ" key_time_point="1009" name="Torso Height" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="z" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxX" key_time_point="1010" name="Thorax Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="x" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxY" key_time_point="1011" name="Thorax Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="y" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="Neck" key_time_point="1012" name="Neck Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_neck" axis="x" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="y" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadWidth" key_time_point="1013" name="Head Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="x" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadDepth" key_time_point="1014" name="Head Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="y" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadHeight" key_time_point="1015" name="Head Length" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="z" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="Shoulders" key_time_point="1016" name="Shoulder Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_shoulder_l" axis="x" key_min="0.9" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_l" axis="y" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_shoulder_r" axis="x" key_min="0.9" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_r" axis="y" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="UpperarmSize" key_time_point="1017" name="Upperarm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_upperarm_l" axis="x" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="y" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_upperarm_r" axis="x" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="y" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="ForearmSize" key_time_point="1018" name="Forearm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_forearm_l" axis="x" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="y" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_forearm_r" axis="x" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="y" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HandSize" key_time_point="1019" name="Hand Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_hand_l" axis="x" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="y" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_hand_r" axis="x" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="y" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <xsl:copy-of select="." />
    </xsl:template>

    <xsl:template match='skin[@name="woman"]/deform_keys/deform_key[@id="weight"]'>
        <deform_key id="TorsoBelly" key_time_point="1001" name="Belly Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_1" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_spine_1" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_spine_1" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoHips" key_time_point="1002" name="Hip Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="x" key_min="0.98" key_max="1.12" />
                <bone_scale bone_type="biped_abdomen" axis="z" key_min="0.97" key_max="1.05" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoButt" key_time_point="1003" name="Butt Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="y" key_min="0.94" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="Thigh" key_time_point="1004" name="Thigh Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thigh_l" axis="x" key_min="0.9" key_max="1.25" />
                <bone_scale bone_type="biped_thigh_l" axis="y" key_min="0.9" key_max="1.35" />
                <bone_scale bone_type="biped_thigh_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_thigh_r" axis="x" key_min="0.9" key_max="1.25" />
                <bone_scale bone_type="biped_thigh_r" axis="y" key_min="0.9" key_max="1.35" />
                <bone_scale bone_type="biped_thigh_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Calf" key_time_point="1005" name="Calf Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_calf_l" axis="x" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="y" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_calf_r" axis="x" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="y" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Foot" key_time_point="1006" name="Foot Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_foot_l" axis="x" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="y" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_foot_r" axis="x" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="y" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoX" key_time_point="1007" name="Torso Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="x" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoY" key_time_point="1008" name="Torso Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="y" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoZ" key_time_point="1009" name="Torso Height" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="z" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxX" key_time_point="1010" name="Thorax Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="x" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxY" key_time_point="1011" name="Thorax Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="y" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="Neck" key_time_point="1012" name="Neck Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_neck" axis="x" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="y" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadWidth" key_time_point="1013" name="Head Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="x" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadDepth" key_time_point="1014" name="Head Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="y" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadHeight" key_time_point="1015" name="Head Length" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="z" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="Shoulders" key_time_point="1016" name="Shoulder Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_shoulder_l" axis="x" key_min="0.9" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_l" axis="y" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_shoulder_r" axis="x" key_min="0.9" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_r" axis="y" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="UpperarmSize" key_time_point="1017" name="Upperarm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_upperarm_l" axis="x" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="y" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_upperarm_r" axis="x" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="y" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="ForearmSize" key_time_point="1018" name="Forearm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_forearm_l" axis="x" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="y" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_forearm_r" axis="x" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="y" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HandSize" key_time_point="1019" name="Hand Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_hand_l" axis="x" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="y" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_hand_r" axis="x" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="y" key_min="0.9" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <xsl:copy-of select="." />
    </xsl:template>

    <xsl:template match='skin[@name="kid_2_male"]/deform_keys/deform_key[@id="weight"]'>
        <deform_key id="TorsoBelly" key_time_point="1001" name="Belly Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_1" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_spine_1" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_spine_1" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoHips" key_time_point="1002" name="Hip Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="x" key_min="0.98" key_max="1.12" />
                <bone_scale bone_type="biped_abdomen" axis="z" key_min="0.97" key_max="1.05" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoButt" key_time_point="1003" name="Butt Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="y" key_min="0.94" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="Thigh" key_time_point="1004" name="Thigh Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thigh_l" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_l" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_thigh_r" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_r" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Calf" key_time_point="1005" name="Calf Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_calf_l" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_calf_r" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Foot" key_time_point="1006" name="Foot Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_foot_l" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_foot_r" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoX" key_time_point="1007" name="Torso Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="x" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoY" key_time_point="1008" name="Torso Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="y" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoZ" key_time_point="1009" name="Torso Height" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="z" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxX" key_time_point="1010" name="Thorax Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="x" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxY" key_time_point="1011" name="Thorax Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="y" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="Neck" key_time_point="1012" name="Neck Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_neck" axis="x" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="y" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadWidth" key_time_point="1013" name="Head Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="x" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadDepth" key_time_point="1014" name="Head Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="y" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadHeight" key_time_point="1015" name="Head Length" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="z" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="Shoulders" key_time_point="1016" name="Shoulder Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_shoulder_l" axis="x" key_min="0.85" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_shoulder_r" axis="x" key_min="0.85" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="UpperarmSize" key_time_point="1017" name="Upperarm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_upperarm_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_upperarm_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="ForearmSize" key_time_point="1018" name="Forearm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_forearm_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_forearm_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HandSize" key_time_point="1019" name="Hand Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_hand_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_hand_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <xsl:copy-of select="." />
    </xsl:template>

    <xsl:template match='skin[@name="kid_2_female"]/deform_keys/deform_key[@id="weight"]'>
        <deform_key id="TorsoBelly" key_time_point="1001" name="Belly Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_1" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_spine_1" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_spine_1" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoHips" key_time_point="1002" name="Hip Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="x" key_min="0.98" key_max="1.12" />
                <bone_scale bone_type="biped_abdomen" axis="z" key_min="0.97" key_max="1.05" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoButt" key_time_point="1003" name="Butt Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="y" key_min="0.94" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="Thigh" key_time_point="1004" name="Thigh Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thigh_l" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_l" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_thigh_r" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_r" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Calf" key_time_point="1005" name="Calf Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_calf_l" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_calf_r" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Foot" key_time_point="1006" name="Foot Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_foot_l" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_foot_r" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoX" key_time_point="1007" name="Torso Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="x" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoY" key_time_point="1008" name="Torso Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="y" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoZ" key_time_point="1009" name="Torso Height" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="z" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxX" key_time_point="1010" name="Thorax Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="x" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxY" key_time_point="1011" name="Thorax Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="y" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="Neck" key_time_point="1012" name="Neck Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_neck" axis="x" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="y" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadWidth" key_time_point="1013" name="Head Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="x" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadDepth" key_time_point="1014" name="Head Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="y" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadHeight" key_time_point="1015" name="Head Length" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="z" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="Shoulders" key_time_point="1016" name="Shoulder Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_shoulder_l" axis="x" key_min="0.85" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_shoulder_r" axis="x" key_min="0.85" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="UpperarmSize" key_time_point="1017" name="Upperarm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_upperarm_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_upperarm_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="ForearmSize" key_time_point="1018" name="Forearm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_forearm_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_forearm_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HandSize" key_time_point="1019" name="Hand Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_hand_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_hand_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <xsl:copy-of select="." />
    </xsl:template>

    <xsl:template match='skin[@name="kid_1_male"]/deform_keys/deform_key[@id="weight"]'>
        <deform_key id="TorsoBelly" key_time_point="1001" name="Belly Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_1" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_spine_1" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_spine_1" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoHips" key_time_point="1002" name="Hip Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="x" key_min="0.98" key_max="1.12" />
                <bone_scale bone_type="biped_abdomen" axis="z" key_min="0.97" key_max="1.05" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoButt" key_time_point="1003" name="Butt Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="y" key_min="0.94" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="Thigh" key_time_point="1004" name="Thigh Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thigh_l" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_l" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_thigh_r" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_r" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Calf" key_time_point="1005" name="Calf Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_calf_l" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_calf_r" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Foot" key_time_point="1006" name="Foot Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_foot_l" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_foot_r" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoX" key_time_point="1007" name="Torso Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="x" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoY" key_time_point="1008" name="Torso Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="y" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoZ" key_time_point="1009" name="Torso Height" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="z" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxX" key_time_point="1010" name="Thorax Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="x" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxY" key_time_point="1011" name="Thorax Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="y" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="Neck" key_time_point="1012" name="Neck Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_neck" axis="x" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="y" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadWidth" key_time_point="1013" name="Head Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="x" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadDepth" key_time_point="1014" name="Head Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="y" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadHeight" key_time_point="1015" name="Head Length" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="z" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="Shoulders" key_time_point="1016" name="Shoulder Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_shoulder_l" axis="x" key_min="0.85" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_shoulder_r" axis="x" key_min="0.85" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="UpperarmSize" key_time_point="1017" name="Upperarm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_upperarm_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_upperarm_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="ForearmSize" key_time_point="1018" name="Forearm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_forearm_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_forearm_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HandSize" key_time_point="1019" name="Hand Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_hand_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_hand_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <xsl:copy-of select="." />
    </xsl:template>

    <xsl:template match='skin[@name="kid_1_female"]/deform_keys/deform_key[@id="weight"]'>
        <deform_key id="TorsoBelly" key_time_point="1001" name="Belly Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_1" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_spine_1" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_spine_1" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoHips" key_time_point="1002" name="Hip Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="x" key_min="0.98" key_max="1.12" />
                <bone_scale bone_type="biped_abdomen" axis="z" key_min="0.97" key_max="1.05" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoButt" key_time_point="1003" name="Butt Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="y" key_min="0.94" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="Thigh" key_time_point="1004" name="Thigh Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thigh_l" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_l" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_thigh_r" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_r" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Calf" key_time_point="1005" name="Calf Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_calf_l" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_calf_r" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Foot" key_time_point="1006" name="Foot Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_foot_l" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_foot_r" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoX" key_time_point="1007" name="Torso Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="x" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoY" key_time_point="1008" name="Torso Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="y" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoZ" key_time_point="1009" name="Torso Height" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="z" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxX" key_time_point="1010" name="Thorax Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="x" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxY" key_time_point="1011" name="Thorax Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="y" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="Neck" key_time_point="1012" name="Neck Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_neck" axis="x" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="y" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadWidth" key_time_point="1013" name="Head Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="x" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadDepth" key_time_point="1014" name="Head Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="y" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadHeight" key_time_point="1015" name="Head Length" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="z" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="Shoulders" key_time_point="1016" name="Shoulder Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_shoulder_l" axis="x" key_min="0.85" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_shoulder_r" axis="x" key_min="0.85" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="UpperarmSize" key_time_point="1017" name="Upperarm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_upperarm_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_upperarm_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="ForearmSize" key_time_point="1018" name="Forearm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_forearm_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_forearm_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HandSize" key_time_point="1019" name="Hand Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_hand_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_hand_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <xsl:copy-of select="." />
    </xsl:template>

    <xsl:template match='skin[@name="kid_3_male"]/deform_keys/deform_key[@id="weight"]'>
        <deform_key id="TorsoBelly" key_time_point="1001" name="Belly Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_1" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_spine_1" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_spine_1" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoHips" key_time_point="1002" name="Hip Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="x" key_min="0.98" key_max="1.12" />
                <bone_scale bone_type="biped_abdomen" axis="z" key_min="0.97" key_max="1.05" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoButt" key_time_point="1003" name="Butt Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="y" key_min="0.94" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="Thigh" key_time_point="1004" name="Thigh Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thigh_l" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_l" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_thigh_r" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_r" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Calf" key_time_point="1005" name="Calf Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_calf_l" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_calf_r" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Foot" key_time_point="1006" name="Foot Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_foot_l" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_foot_r" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoX" key_time_point="1007" name="Torso Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="x" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoY" key_time_point="1008" name="Torso Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="y" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoZ" key_time_point="1009" name="Torso Height" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="z" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxX" key_time_point="1010" name="Thorax Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="x" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxY" key_time_point="1011" name="Thorax Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="y" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="Neck" key_time_point="1012" name="Neck Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_neck" axis="x" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="y" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadWidth" key_time_point="1013" name="Head Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="x" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadDepth" key_time_point="1014" name="Head Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="y" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadHeight" key_time_point="1015" name="Head Length" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="z" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="Shoulders" key_time_point="1016" name="Shoulder Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_shoulder_l" axis="x" key_min="0.85" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_shoulder_r" axis="x" key_min="0.85" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="UpperarmSize" key_time_point="1017" name="Upperarm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_upperarm_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_upperarm_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="ForearmSize" key_time_point="1018" name="Forearm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_forearm_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_forearm_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HandSize" key_time_point="1019" name="Hand Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_hand_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_hand_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <xsl:copy-of select="." />
    </xsl:template>

    <xsl:template match='skin[@name="kid_3_female"]/deform_keys/deform_key[@id="weight"]'>
        <deform_key id="TorsoBelly" key_time_point="1001" name="Belly Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_1" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_spine_1" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_spine_1" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoHips" key_time_point="1002" name="Hip Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="x" key_min="0.98" key_max="1.12" />
                <bone_scale bone_type="biped_abdomen" axis="z" key_min="0.97" key_max="1.05" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoButt" key_time_point="1003" name="Butt Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="y" key_min="0.94" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="Thigh" key_time_point="1004" name="Thigh Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thigh_l" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_l" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_thigh_r" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_r" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Calf" key_time_point="1005" name="Calf Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_calf_l" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_calf_r" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Foot" key_time_point="1006" name="Foot Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_foot_l" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_foot_r" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoX" key_time_point="1007" name="Torso Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="x" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoY" key_time_point="1008" name="Torso Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="y" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoZ" key_time_point="1009" name="Torso Height" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="z" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxX" key_time_point="1010" name="Thorax Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="x" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxY" key_time_point="1011" name="Thorax Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="y" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="Neck" key_time_point="1012" name="Neck Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_neck" axis="x" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="y" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadWidth" key_time_point="1013" name="Head Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="x" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadDepth" key_time_point="1014" name="Head Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="y" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadHeight" key_time_point="1015" name="Head Length" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="z" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="Shoulders" key_time_point="1016" name="Shoulder Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_shoulder_l" axis="x" key_min="0.85" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_shoulder_r" axis="x" key_min="0.85" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="UpperarmSize" key_time_point="1017" name="Upperarm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_upperarm_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_upperarm_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="ForearmSize" key_time_point="1018" name="Forearm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_forearm_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_forearm_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HandSize" key_time_point="1019" name="Hand Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_hand_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_hand_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <xsl:copy-of select="." />
    </xsl:template>

    <xsl:template match='skin[@name="toddler_male"]/deform_keys/deform_key[@id="weight"]'>
        <deform_key id="TorsoBelly" key_time_point="1001" name="Belly Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_1" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_spine_1" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_spine_1" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoHips" key_time_point="1002" name="Hip Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="x" key_min="0.98" key_max="1.12" />
                <bone_scale bone_type="biped_abdomen" axis="z" key_min="0.97" key_max="1.05" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoButt" key_time_point="1003" name="Butt Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="y" key_min="0.94" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="Thigh" key_time_point="1004" name="Thigh Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thigh_l" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_l" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_thigh_r" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_r" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Calf" key_time_point="1005" name="Calf Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_calf_l" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_calf_r" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Foot" key_time_point="1006" name="Foot Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_foot_l" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_foot_r" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoX" key_time_point="1007" name="Torso Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="x" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoY" key_time_point="1008" name="Torso Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="y" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoZ" key_time_point="1009" name="Torso Height" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="z" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxX" key_time_point="1010" name="Thorax Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="x" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxY" key_time_point="1011" name="Thorax Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="y" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="Neck" key_time_point="1012" name="Neck Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_neck" axis="x" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="y" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadWidth" key_time_point="1013" name="Head Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="x" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadDepth" key_time_point="1014" name="Head Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="y" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadHeight" key_time_point="1015" name="Head Length" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="z" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="Shoulders" key_time_point="1016" name="Shoulder Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_shoulder_l" axis="x" key_min="0.85" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_shoulder_r" axis="x" key_min="0.85" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="UpperarmSize" key_time_point="1017" name="Upperarm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_upperarm_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_upperarm_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="ForearmSize" key_time_point="1018" name="Forearm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_forearm_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_forearm_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HandSize" key_time_point="1019" name="Hand Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_hand_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_hand_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <xsl:copy-of select="." />
    </xsl:template>

    <xsl:template match='skin[@name="toddler_female"]/deform_keys/deform_key[@id="weight"]'>
        <deform_key id="TorsoBelly" key_time_point="1001" name="Belly Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_1" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_spine_1" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_spine_1" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoHips" key_time_point="1002" name="Hip Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="x" key_min="0.98" key_max="1.12" />
                <bone_scale bone_type="biped_abdomen" axis="z" key_min="0.97" key_max="1.05" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoButt" key_time_point="1003" name="Butt Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_abdomen" axis="y" key_min="0.94" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="Thigh" key_time_point="1004" name="Thigh Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thigh_l" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_l" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_thigh_r" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_r" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_thigh_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Calf" key_time_point="1005" name="Calf Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_calf_l" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_calf_r" axis="x" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="y" key_min="0.9" key_max="1.2" />
                <bone_scale bone_type="biped_calf_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="Foot" key_time_point="1006" name="Foot Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_foot_l" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_foot_r" axis="x" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="y" key_min="0.8" key_max="1.2" />
                <bone_scale bone_type="biped_foot_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoX" key_time_point="1007" name="Torso Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="x" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoY" key_time_point="1008" name="Torso Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="y" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="TorsoZ" key_time_point="1009" name="Torso Height" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_spine_2" axis="z" key_min="1" key_max="1.15" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxX" key_time_point="1010" name="Thorax Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="x" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="ThoraxY" key_time_point="1011" name="Thorax Depth" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_thorax" axis="y" key_min="1" key_max="1.2" />
                <bone_scale bone_type="biped_thorax" axis="z" key_min="1" key_max="1.02" />
            </bone_scales>
        </deform_key>
        <deform_key id="Neck" key_time_point="1012" name="Neck Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_neck" axis="x" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="y" key_min="1" key_max="1.15" />
                <bone_scale bone_type="biped_neck" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadWidth" key_time_point="1013" name="Head Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="x" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadDepth" key_time_point="1014" name="Head Width" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="y" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="HeadHeight" key_time_point="1015" name="Head Length" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_head" axis="z" key_min="1.00" key_max="1.12" />
            </bone_scales>
        </deform_key>
        <deform_key id="Shoulders" key_time_point="1016" name="Shoulder Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_shoulder_l" axis="x" key_min="0.85" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_shoulder_r" axis="x" key_min="0.85" key_max="1.075" />
                <bone_scale bone_type="biped_shoulder_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_shoulder_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="UpperarmSize" key_time_point="1017" name="Upperarm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_upperarm_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_upperarm_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_upperarm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="ForearmSize" key_time_point="1018" name="Forearm Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_forearm_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_forearm_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_forearm_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <deform_key id="HandSize" key_time_point="1019" name="Hand Size" group_id="0">
            <bone_scales>
                <bone_scale bone_type="biped_hand_l" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_l" axis="z" key_min="1" key_max="1" />
                <bone_scale bone_type="biped_hand_r" axis="x" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="y" key_min="0.85" key_max="1.15" />
                <bone_scale bone_type="biped_hand_r" axis="z" key_min="1" key_max="1" />
            </bone_scales>
        </deform_key>
        <xsl:copy-of select="." />
    </xsl:template>
</xsl:stylesheet>
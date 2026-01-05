<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output omit-xml-declaration="yes" />
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='voice_definition[@only_for_npcs="true"]'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:attribute name="only_for_npcs">false</xsl:attribute>
            <xsl:copy-of select="node()"></xsl:copy-of>
        </xsl:copy>
    </xsl:template>
</xsl:stylesheet>
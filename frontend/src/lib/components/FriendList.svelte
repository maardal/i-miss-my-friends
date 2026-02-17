<script lang="ts">
	import type { LovedOne } from '$lib/types/types';
	import LovedOneCard from './LovedOneCard.svelte';

	const data = $props<{ lovedOnes: LovedOne[] }>();
	const lovedOnes = data.lovedOnes;
</script>

{#if lovedOnes && lovedOnes.length > 0}
	{#each lovedOnes as loved}
		<LovedOneCard {loved}></LovedOneCard>
	{/each}
{:else}
	<p>No loved ones found</p>
{/if}

<script lang="ts">
	let data = $props();
	const { lovedones } = data.lovedones;
	console.log(lovedones);
</script>

{#each data.lovedones as lovedOne}
	<p>{lovedOne.relationship}: {lovedOne.name} + {lovedOne.lastHangout}</p>
{/each}
